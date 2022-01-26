using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
	public PEON_STATE stateid;

	public State()
	{
	}

	public abstract void Enter(Peon p);
	public abstract void Execute(Peon p);
	public abstract void Exit(Peon p);
}
