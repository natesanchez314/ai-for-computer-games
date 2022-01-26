using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peon_AtForest : State
{
    private static Peon_AtForest instance;

    public Peon_AtForest()
    {
        stateid = PEON_STATE.AT_FOREST;
    }

    public static Peon_AtForest Instance()
    {
        if (instance == null)
        {
            instance = new Peon_AtForest();
        }
        return instance;
    }

    public override void Enter(Peon p)
    {
        Debug.Log("At Forest!");
    }

    public override void Execute(Peon p)
    {
        if (p.heldWood >= p.maxWood)
        {
            p.FSM_Peon.ChangeState("Moving_To_Castle", p);
        }
        else
        {
            p.ChopWood();
        }
    }

    public override void Exit(Peon p)
    {
        Debug.Log("Leaving forest");
    }
}
