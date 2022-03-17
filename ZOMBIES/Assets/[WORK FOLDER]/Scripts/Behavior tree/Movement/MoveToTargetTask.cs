using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MoveToTargetTask : SelectorTask
{
    public MoveToTargetTask() : base()
    { }

    protected override void InitChildren()
    {
        this.AddTask(new PathFindingTask());
        //this.children.Add(new BuildPathTask());
        //this.children.Add(new FollowPathTask());
        //this.children.Add(new SteeringTask());
        this.children.Add(new AvoidZombiesTask());
        //this.children.Add(new AvoidDeadEndTask());
    }
}
