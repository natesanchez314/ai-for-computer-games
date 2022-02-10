using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AvoidZombiesTask : SequenceTask
{
    public AvoidZombiesTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        //Debug.Log("Avoiding zombies");
        return base.Run(survivor);
    }

    protected override void InitChildren()
    {
        AddTask(new AvoidMainTask());
        AddTask(new AvoidOthersTask());
        AddTask(new MoveTask());
    }
}
