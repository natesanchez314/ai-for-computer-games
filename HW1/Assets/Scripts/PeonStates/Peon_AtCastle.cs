using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Peon_AtCastle : State
{
	private static Peon_AtCastle instance;
	public Peon_AtCastle()
	{
		stateid = PEON_STATE.AT_CASTLE;
	}

	public static Peon_AtCastle Instance()
	{
		if (instance == null)
		{
			instance = new Peon_AtCastle();
		}
		return instance;

	}

	public override void Enter(Peon p)
	{
		Debug.Log("At Castle!");
	}

	public override void Execute(Peon p)
	{
        p.DropOffGold();
		if (p.preferGold) p.FSM_Peon.ChangeState("Moving_To_Mine", p);
		else p.FSM_Peon.ChangeState("Moving_To_Forest", p);
	}

	public override void Exit(Peon p)
	{
		Debug.Log("Leaving Castle");
	}
}
