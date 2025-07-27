using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : AICharacter, IMoveable, IDamageable, IAttackable, IDieable
{
	[SerializeField] protected AnimationClip hurtClip;
	protected Quaternion? targetRotation = null;
	protected Quaternion? selfToPlayerRotation = null;
	protected bool canMove = false;
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
	}

	public void Move(in Vector3 moveDirection)
	{
		Rotate(moveDirection);
		if (canMove)
		{
			Debug.Log("isMoving");
			characterController.Move(transform.forward * Time.deltaTime * speed);
		}
	}

	protected void Rotate(in Vector3 rotateDirection)
	{
		if (!canMove) {
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
		Ally ally = FindFirstObjectByType<Ally>();
		MainCharacter mainCharacter = FindFirstObjectByType<MainCharacter>();
		Vector3? allyPos = ally == null ? null : ally.gameObject.transform.position;
		Vector3? mainCharPos = mainCharacter == null ? null : mainCharacter.gameObject.transform.position;
		float toAllyDis = allyPos.HasValue ? Vector3.Distance(transform.position, allyPos.Value) : 1000000.0f;
		float toMainCharDis = mainCharPos.HasValue ? Vector3.Distance(transform.position, mainCharPos.Value) : 1000000.0f;
		Vector3? target = toAllyDis < toMainCharDis ? allyPos : mainCharPos;
		if (target.HasValue)
		{
			Vector3 direction3D = target.Value - transform.position;
			Vector2 point = new Vector2(direction3D.x, direction3D.z);
			float dot = Vector2.Dot(point.normalized, Vector2.up);
			float cos = Mathf.Clamp(dot, -1.0f, 1.0f);
			float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;
			angle = point.x > 0 ? angle : -angle;
			selfToPlayerRotation = Quaternion.Euler(0.0f, angle, 0.0f);
		}
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
		canMove = false;
		CalSelfToOpponentRotation();
		canLookAtOpponent = true;
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
