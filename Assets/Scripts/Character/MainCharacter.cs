using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacter : Character, IMoveable, IDamageable, IAttackable
{
    [SerializeField] private InputActionReference IA_Ref;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    protected override void Start()
    {
		base.Start();
	}

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (IA_Ref)
        {
			Vector2 moveDirection = IA_Ref.action.ReadValue<Vector2>();
            if(moveDirection.x != 0.0f || moveDirection.y != 0.0f)
            {
				animator?.SetBool("isMoving", true);
            }
            else animator?.SetBool("isMoving", false);
			transform.Translate(new Vector3(moveDirection.x * speed * Time.deltaTime, 0.0f, moveDirection.y * speed * Time.deltaTime));
		}
    }

    public void Attack()
    {

    }

    public void TakeDamage(int damageAmount, in GameObject instigator, in GameObject damageCauser)
    {

	}
}
