using System;
using System.Collections;
using UnityEngine;

public class Ally : AICharacter, IMoveable, IDamageable, IAttackable
{
	[SerializeField] protected AnimationClip hurtClip;
	[SerializeField] protected AnimationClip attackClip;
	[SerializeField] protected Vector3 detectBoxSize;
	[SerializeField] protected float maxDetectDis;
	protected Quaternion? targetRotation = null;
	protected Quaternion? selfToPlayerRotation = null;
	protected bool canMove = false;
	protected bool canAttack = true;
	protected bool canLookAtOpponent = false;
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
		LookAtOpponent();
		if (canAttack) DetectToAttack();
	}

	public void Move(in Vector3 moveDirection)
	{
		Rotate(moveDirection);
		if (canMove)
		{
			characterController.Move(transform.forward * Time.deltaTime * speed);
		}
	}

	protected void Rotate(in Vector3 rotateDirection)
	{
		if (!canMove)
		{
			if (!targetRotation.HasValue)
			{
				Vector2 point = new Vector2(rotateDirection.x, rotateDirection.z);
				float dot = Vector2.Dot(point, Vector2.up);
				float mag = point.magnitude * Vector2.up.magnitude;
				float cos = Mathf.Clamp(dot / mag, -1.0f, 1.0f);
				float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;
				angle = rotateDirection.x > 0 ? angle : -angle;
				targetRotation = Quaternion.Euler(0.0f, angle + transform.eulerAngles.y, 0.0f);
			}
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation.Value, 10.0f * Time.deltaTime);
			if (Quaternion.Angle(transform.rotation, targetRotation.Value) < 0.1f)
			{
				targetRotation = null;
				canMove = true;
			}
		}
	}

	protected void LookAtOpponent()
	{
		if (canLookAtOpponent)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, selfToPlayerRotation.Value, 10.0f * Time.deltaTime);
			if (Quaternion.Angle(transform.rotation, selfToPlayerRotation.Value) < 0.1f)
			{
				canLookAtOpponent = false;
			}
		}
	}

	protected void CalSelfToOpponentRotation()
	{
		Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
		float minToEnemyDis = 10000000.0f;
		Vector3 target = gameObject.transform.position;
		for (int i = 0; i < enemies.Length; i++) { 
			Enemy enemy = enemies[i];
			Vector3 enemyPos = enemy.gameObject.transform.position;
			float toEnemyDis = Vector3.Distance(transform.position, enemyPos);
			target = toEnemyDis < minToEnemyDis ? enemyPos : target;
		}
		Vector3 direction3D = target - transform.position;
		Vector2 point = new Vector2(direction3D.x, direction3D.z);
		float dot = Vector2.Dot(point.normalized, Vector2.up);
		float cos = Mathf.Clamp(dot, -1.0f, 1.0f);
		float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;
		angle = point.x > 0 ? angle : -angle;
		selfToPlayerRotation = Quaternion.Euler(0.0f, angle, 0.0f);
	}

	protected bool DetectToAttack()
	{
		LayerMask mask = 1 << LayerMask.NameToLayer("Character");
		RaycastHit hit;
		bool detected = Physics.BoxCast(transform.position, detectBoxSize / 2f, transform.forward, out hit, Quaternion.identity, maxDetectDis, mask);
		if (detected && hit.collider.gameObject.tag != gameObject.tag)
		{
			Character character = hit.collider.gameObject?.GetComponent<Character>();
			if (character != null)
			{
				CustomStateMachine customStateMachine = character.gameObject.gameObject.GetComponent<CustomStateMachine>();
				if (customStateMachine?.GetCurrentState().GetType() != typeof(DieState))
				{
					stateMachine.ChangeState(new AttackState(this));
					canAttack = false;
					StartCoroutine(AttackRecover(restDuration));
					return true;
				}
			}
		}
		return false;
	}

	protected IEnumerator AttackRecover(float delay)
	{
		yield return new WaitForSeconds(delay);
		canAttack = true;
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
		if (attackClip) stateMachine.ChangeStateDelay(new IdleState(gameObject), attackClip.length - 0.5f);
	}

	public int GetDamage()
	{
		return 5;
	}

	protected IEnumerator MoveToIdle(float delay)
	{
		yield return new WaitForSeconds(delay);
		stateMachine.ChangeState(new IdleState(gameObject));
		canMove = false;
		CalSelfToOpponentRotation();
		canLookAtOpponent = true;
		StartCoroutine(MakeDecisionAfterDelay(restDuration));
	}


	public void Die()
	{
		stateMachine.ChangeState(new DieState(this));
		StopAllCoroutines();
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
