using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Peon_Digging_For_Gold : State
{
    private static Peon_Digging_For_Gold instance;

    public Peon_Digging_For_Gold()
    {
        stateid = PEON_STATE.DIGGING_FOR_GOLD;
    }

    public static Peon_Digging_For_Gold Instance()
    {
        if (instance == null)
        {
            instance = new Peon_Digging_For_Gold();
        }
        return instance;
    }

    public override void Enter(Peon p)
    {
        Debug.Log("Diggin for gold!");
    }

    public override void Execute(Peon p)
    {
        if (p.heldGold >= p.maxGold) p.FSM_Peon.ChangeState("Exit_Mine", p);
        else p.DigForGold();
    }

    public override void Exit(Peon p)
    {
        Debug.Log("Leaving Mine");
    }
}
