using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Peon_AtMine : State
{
    private static Peon_AtMine instance;

    public Peon_AtMine()
    {
        stateid = PEON_STATE.AT_CASTLE;
    }

    public static Peon_AtMine Instance()
    {
        if (instance == null)
        {
            instance = new Peon_AtMine();
        }
        return instance;
    }

    public override void Enter(Peon p)
    {
        Debug.Log("At Mine!");

        // Add New states to the FSM
        if (!p.FSM_Peon.HasState("Enter_Mine")) p.FSM_Peon.AddState("Enter_Mine", Peon_Enter_Mine.Instance());
        if (!p.FSM_Peon.HasState("Dig_At_Mine")) p.FSM_Peon.AddState("Dig_At_Mine", Peon_Digging_For_Gold.Instance());
        if (!p.FSM_Peon.HasState("Exit_Mine")) p.FSM_Peon.AddState("Exit_Mine", Peon_Exit_Mine.Instance());
        // Peon enters the "Enter Mine" state
        if (p.heldGold < p.maxGold) 
            p.FSM_Peon.ChangeState("Enter_Mine", p);

    }

    public override void Execute(Peon p)
    {
        // To Do
        //if (p.heldGold >= p.maxGold) p.FSM_Peon.ChangeState("Moving_To_Castle", p);
    }

    public override void Exit(Peon p)
    {
        Debug.Log("Leaving Mine");
    }
}
