using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peon_Enter_Mine : State
{
    private static Peon_Enter_Mine instance;

    public Peon_Enter_Mine()
    {
        stateid = PEON_STATE.ENTER_MINE;
    }

    public static Peon_Enter_Mine Instance()
    {
        if (instance == null)
        {
            instance = new Peon_Enter_Mine();
        }
        return instance;
    }

    public override void Enter(Peon p)
    {
        Debug.Log("Entering mine");
    }

    public override void Execute(Peon p)
    {
        p.FSM_Peon.ChangeState("Dig_At_Mine", p);
    }

    public override void Exit(Peon p)
    {
        Debug.Log("Starting to dig for gold");
    }
}
