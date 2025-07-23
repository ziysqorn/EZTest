using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacter : Character, IMoveable, IDamageable, IAttackable
{
	private Animator animator;
    private PlayerInput playerInput;
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
        
	}

    public void Move()
    {
        InputAction inputAction = playerInput.actions.FindAction("Move");
        if (inputAction != null) {
			Vector2 moveDirection = inputAction.ReadValue<Vector2>();

			if (moveDirection.x + moveDirection.y != 0.0f)
			{
				animator?.SetBool("isMoving", true);
				Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0.0f, moveDirection.y));
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10.0f * Time.deltaTime);
				transform.Translate(Vector3.forward * Time.deltaTime * speed);
			}
			else animator?.SetBool("isMoving", false);
		}
	}

    public void Attack()
    {
		InputAction inputAction = playerInput.actions.FindAction("Attack");
        if (inputAction != null) {
			bool isTrigger = inputAction.triggered;
			if (isTrigger) Debug.Log("Attack");
		}   
    }

    public void TakeDamage(int damageAmount, in GameObject instigator, in GameObject damageCauser)
    {

	}
}
