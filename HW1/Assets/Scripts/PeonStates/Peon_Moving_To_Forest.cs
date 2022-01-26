using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Peon_Moving_To_Forest : State
{
    private static Peon_Moving_To_Forest instance;
    
    public Peon_Moving_To_Forest()
    {
        stateid = PEON_STATE.MOVING_TO_FOREST;
    }

    public static Peon_Moving_To_Forest Instance()
    {
        if (instance == null)
        {
            instance = new Peon_Moving_To_Forest();
        }
        return instance;
    }

    public override void Enter(Peon p)
    {
        Debug.Log("Moving to forest!");
    }

    public override void Execute(Peon p)
    {
        bool atForest = p.MoveToForest();

        if (atForest == true)
        {
            p.FSM_Peon.ChangeState("At_Forest", p);
        }
        else
        {
            Debug.Log("Moving to forest");
        }
    }

    public override void Exit(Peon p)
    {
        Debug.Log("Arrived at forest");
    }
}
