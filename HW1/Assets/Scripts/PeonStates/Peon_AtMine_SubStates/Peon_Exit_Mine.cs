using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Peon_Exit_Mine : State
{
    private static Peon_Exit_Mine instance;

    public Peon_Exit_Mine()
    {
        stateid = PEON_STATE.EXIT_MINE;
    }

    public static Peon_Exit_Mine Instance()
    {
        if (instance == null)
        {
            instance = new Peon_Exit_Mine();
        }
        return instance;
    }

    public override void Enter(Peon p)
    {
        Debug.Log("Exiting mine");
    }

    public override void Execute(Peon p)
    {
        p.FSM_Peon.ChangeState("Moving_To_Castle", p);
        //p.FSM_Peon.ChangeState("At_Mine", p);
    }

    public override void Exit(Peon p)
    {
        Debug.Log("Exited Mine");
    }
}
