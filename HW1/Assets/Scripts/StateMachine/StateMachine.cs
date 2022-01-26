using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
	protected Dictionary<string, State> states;

	//protected T owner;

	protected State prevState;
	protected State currentState;


	public StateMachine()
	{
		states = new Dictionary<string, State>();
	}

	public void AddState(string name, State newState)
    {
		states.Add(name, newState);
    }

	public bool HasState(string name)
    {
		return states.ContainsKey(name);
    }

	public void ChangeState(string _state, Peon entity)
	{
		State newState = states[_state];

		prevState = currentState;

		if (currentState != null)
		{
			currentState.Exit(entity);
		}

		currentState = newState;

		currentState.Enter(entity);
	}

	public void Update(Peon entity)
	{
		currentState.Execute(entity);

	}


}
