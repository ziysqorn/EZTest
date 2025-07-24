using System.Collections;
using UnityEngine;

public class CustomStateMachine : MonoBehaviour
{
	private ICharacterState currentState;

	public void ChangeState(ICharacterState state)
	{
		if (currentState == null)
		{
			currentState = state;
		}
		else if (currentState.CanSwitchTo(state))
		{
			currentState.Exit();
			currentState = state;
			if (currentState != null) currentState.Enter();
		}
	}

	public IEnumerator ChangeStateDelay(ICharacterState state, float delay)
	{
		yield return new WaitForSeconds(delay);
		if (currentState == null)
		{
			currentState = state;
		}
		else if (currentState.CanSwitchTo(state))
		{
			currentState.Exit();
			currentState = state;
			if (currentState != null) currentState.Enter();
		}
	}

	public ICharacterState GetCurrentState()
	{
		return currentState;
	}

	public void Update()
	{
		if (currentState != null)
		{
			currentState.Update();
		}
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }
}
