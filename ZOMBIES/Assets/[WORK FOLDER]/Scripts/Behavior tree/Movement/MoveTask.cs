using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MoveTask : Task
{
    public MoveTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        survivor.MoveTo(survivor.blackboard.heading);
        survivor.blackboard.heading = survivor.transform.position;
        return true;
    }
}
