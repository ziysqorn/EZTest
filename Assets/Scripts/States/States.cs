using UnityEngine;

public class IdleState : ICharacterState
{
	private GameObject owner;
	public IdleState(GameObject gameObject)
	{
		owner = gameObject;
	}
	public void Enter()
	{

	}
	public void Update()
	{

	}
	public void Exit()
	{

	}

	public bool CanSwitchTo(ICharacterState inState)
	{
		return true;
	}
}

public class WalkState : ICharacterState
{
	private Character owner;
	protected Vector3 moveDirection;
	public WalkState(Character inOwner, Vector3 moveDir)
	{
		owner = inOwner;
		moveDirection = moveDir;
	}
	public void Enter()
	{
		IMoveable moveable = owner as IMoveable;
		if (moveable != null) {
			owner.animator.SetBool("isMoving", true);
			moveable.Move(moveDirection);
		}
	}
	public void Update()
	{

	}
	public void Exit()
	{
		IMoveable moveable = owner as IMoveable;
		if (moveable != null)
		{
			owner.animator.SetBool("isMoving", false);
		}
	}

	public bool CanSwitchTo(ICharacterState inState)
	{
		if (inState.GetType() == typeof(WalkState) || inState.GetType() == typeof(IdleState)) return true;
		return false;
	}
}

public class AttackState : ICharacterState
{
	private Character owner;
	public AttackState(Character inOwner)
	{
		owner = inOwner;
	}
	public void Enter()
	{
		IAttackable attackable = owner as IAttackable;
		if (attackable != null)
		{
			owner.animator.SetBool("isAttacking", true);
			attackable.Attack();
		}
	}
	public void Update()
	{

	}
	public void Exit()
	{
		IAttackable attackable = owner as IAttackable;
		if (attackable != null)
		{
			owner.animator.SetBool("isAttacking", false);
		}
	}
	public bool CanSwitchTo(ICharacterState inState)
	{
		if (inState.GetType() == typeof(IdleState)) return true;
		return false;
	}
}

public class HurtState : ICharacterState
{
	private Character owner;
	public HurtState(Character inOwner)
	{
		owner = inOwner;
	}
	public void Enter()
	{
		IDamageable damageable = owner as IDamageable;
		if (damageable != null)
		{
			owner.animator.SetBool("isHurt", true);
		}
	}
	public void Update()
	{

	}
	public void Exit()
	{
		IDamageable damageable = owner as IDamageable;
		if (damageable != null)
		{
			owner.animator.SetBool("isHurt", false);
		}
	}
	public bool CanSwitchTo(ICharacterState inState)
	{
		return true;
	}
}

public class DieState : ICharacterState
{
	private Character owner;
	public DieState(Character inOwner)
	{
		owner = inOwner;
	}
	public void Enter()
	{
		owner.animator.SetBool("isDead", true);
	}
	public void Update()
	{

	}
	public void Exit()
	{
		owner.animator.SetBool("isDead", false);
	}
	public bool CanSwitchTo(ICharacterState inState)
	{
		return true;
	}
}


