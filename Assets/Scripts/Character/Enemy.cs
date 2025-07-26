using System;
using System.Collections;
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
		stateMachine.GetCurrentState().Update();
    }

	public void Move(in Vector3 moveDirection)
	{
		transform.Translate(moveDirection * Time.deltaTime * speed);
	}

	protected override void MakeDecision()
	{
		int directionChoosing = UnityEngine.Random.Range(0, moveDirections.Count);
		Vector3 chosenDirection = moveDirections[directionChoosing];
		stateMachine.ChangeState(new WalkState(this, chosenDirection));
		StartCoroutine(MoveToIdle(moveTime));
	}

	public void Attack()
	{

	}

	protected IEnumerator MoveToIdle(float delay)
	{
		yield return new WaitForSeconds(delay);
		stateMachine.ChangeState(new IdleState(gameObject));
		StartCoroutine(MakeDecisionAfterDelay(restDuration));

	}


	public void Die()
	{
		stateMachine.ChangeState(new DieState(this));
		GameInstance.instance?.DecreaseEnemyCount(1);
		Debug.Log("Enemy died");
	}

	public void TakeDamage(int damageAmount, in GameObject instigator, in GameObject damageCauser)
	{
		Type currentStateType = stateMachine.GetCurrentState().GetType();
		if (currentStateType != typeof(HurtState) && currentStateType != typeof(DieState))
		{
			curHealth -= damageAmount;
			if (curHealth <= 0)
			{
				Die();
			}
			else
			{
				stateMachine.ChangeState(new HurtState(this));
				if (hurtClip) StartCoroutine(stateMachine.ChangeStateDelay(new IdleState(gameObject), hurtClip.length));
			}
		}
	}
}
