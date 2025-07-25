using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainCharacter : Character, IMoveable, IDamageable, IAttackable
{
    private PlayerInput playerInput;
    [SerializeField] protected AnimationClip attackClip;
    public Camera characterCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }
    protected override void Start()
    {
		base.Start();
        stateMachine.ChangeState(new IdleState(gameObject));
	}

    // Update is called once per frame
    void Update()
    {
        CheckInput();   
	}
	public void Move(in Vector3 moveDirection)
    {
		Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10.0f * Time.deltaTime);
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}

    public void Attack()
    {
        Debug.Log("Attack");
        if (attackClip) StartCoroutine(stateMachine.ChangeStateDelay(new IdleState(gameObject), attackClip.length - 0.3f));
	}

    public void TakeDamage(int damageAmount, in GameObject instigator, in GameObject damageCauser)
    {

	}

    public void CheckInput()
    {
		InputAction moveAction = playerInput.actions.FindAction("Move");
		InputAction attackAction = playerInput.actions.FindAction("Attack");
        if(moveAction != null)
        {
            Vector2 moveDirection = moveAction.ReadValue<Vector2>();
            if(moveDirection.x + moveDirection.y != 0.0f)
            {
				stateMachine.ChangeState(new WalkState(this, new Vector3(moveDirection.x, 0.0f, moveDirection.y)));
            }
            else if(stateMachine.GetCurrentState().GetType() == typeof(WalkState))
            {
                stateMachine.ChangeState(new IdleState(gameObject));
            }
			
		}
        if(attackAction != null)
        {
            bool isTrigger = attackAction.triggered;
            if (isTrigger)
            {
				stateMachine.ChangeState(new AttackState(this));
			}
        }
	}
}
