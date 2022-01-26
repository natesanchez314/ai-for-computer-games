using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Peon_Moving_To_Mine : State
{
	private static Peon_Moving_To_Mine instance;
	public Peon_Moving_To_Mine() {
		stateid = PEON_STATE.MOVING_TO_MINE;
	}

	public static Peon_Moving_To_Mine Instance()
	{
		if (instance == null)
		{
			instance = new Peon_Moving_To_Mine();
		}
		return instance;

	}

	public override void Enter(Peon p)
	{
		Debug.Log("Moving to mine!");
	}

	public override void Execute(Peon p)
	{
        bool atMine = p.MoveToMine();

        if(atMine == true)
        {
            p.FSM_Peon.ChangeState("At_Mine", p);

        }
        else
        {
            Debug.Log("Moving To Mine");
        }
	}

	public override void Exit(Peon p)
	{
		Debug.Log("Arrived at Mine!");
	}
}
