using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WanderTask : Task
{
    public WanderTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        Debug.Log("Wandering");
        return base.Run(survivor);
    }

    protected override void InitChildren()
    {
        AddTask(new SetNewWanderHeadingTask());
        AddTask(new WanderMoveTask());
    }
}
