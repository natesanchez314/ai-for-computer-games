using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Peon_Moving_To_Castle : State
{
	private static Peon_Moving_To_Castle instance;
	public Peon_Moving_To_Castle() {
		stateid = PEON_STATE.MOVING_TO_CASTLE;
	}

	public static Peon_Moving_To_Castle Instance()
	{
		if (instance == null)
		{
			instance = new Peon_Moving_To_Castle();
		}
		return instance;

	}

	public override void Enter(Peon p)
	{
		Debug.Log("Heading to Castle");
	}

	public override void Execute(Peon p)
	{
		bool atMine = p.MoveToCastle();

        if (atMine == true)
        {
            p.FSM_Peon.ChangeState("At_Castle", p);

        }
        else
        {
            Debug.Log("Moving To Castle");
        }
    }

	public override void Exit(Peon p)
	{
		Debug.Log("Arrived at Castle!");
	}
}
