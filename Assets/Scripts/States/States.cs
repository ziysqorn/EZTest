using UnityEngine;

public class CustomStateMachine
{
	private ICharacterState currentState;

	public void ChangeState(ICharacterState state)
	{
		if(currentState != null)
		{
			currentState.Exit();
		}
		currentState = state;
		if (currentState != null) { 
			currentState.Enter();
		}
	}

	public void Update()
	{
		if (currentState != null)
		{
			currentState.Update();
		}
	}
}
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
}

public class WalkState : ICharacterState
{
	private GameObject owner;
	public WalkState(GameObject gameObject)
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
}

public class AttackState : ICharacterState
{
	private GameObject owner;
	public AttackState(GameObject gameObject)
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
}


