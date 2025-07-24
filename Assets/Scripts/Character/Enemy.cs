using UnityEngine;

public class Enemy : AICharacter, IMoveable, IDamageable, IAttackable, IDieable
{
	[SerializeField] protected AnimationClip hurtClip;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Awake()
	{
		animator = GetComponent<Animator>();
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

	public void Move(in Vector3 moveDirection)
	{

	}

	public void Attack()
	{

	}

	public void Die()
	{
		stateMachine.ChangeState(new DieState(this));
		Debug.Log("Enemy died");
	}

	public void TakeDamage(int damageAmount, in GameObject instigator, in GameObject damageCauser)
	{
		curHealth -= damageAmount;
		if (curHealth <= 0) {
			Die();
		}
		else
		{
			stateMachine.ChangeState(new HurtState(this));
			if(hurtClip) StartCoroutine(stateMachine.ChangeStateDelay(new IdleState(gameObject), hurtClip.length));
		}
	}
}
