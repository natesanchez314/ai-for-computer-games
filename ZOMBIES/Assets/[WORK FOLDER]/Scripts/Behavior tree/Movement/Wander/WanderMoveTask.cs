using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WanderMoveTask : Task
{
    public WanderMoveTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        survivor.MoveTo(survivor.blackboard.targetPathNode.transform.position);
        return true;
    }
}
