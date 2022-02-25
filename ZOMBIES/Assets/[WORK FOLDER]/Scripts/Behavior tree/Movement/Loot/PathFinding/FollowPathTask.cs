using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FollowPathTask : Task
{
    public FollowPathTask()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        // Reached end node
        if (survivor.blackboard.path == null || survivor.blackboard.path.Count == 0)
            return false;
        else
        {
            Vector3 survivorPos = survivor.transform.position;
            Vector3 targetPos = survivor.blackboard.path[0].transform.position;
            float dist = (targetPos - survivorPos).magnitude;
            // We've reached a node in our path
            if (dist < 3.0f)
                survivor.blackboard.path.RemoveAt(0);
            if (survivor.blackboard.path.Count == 0)
                return false;
            else
            {
                survivor.MoveTo(survivor.blackboard.path[0].transform.position);
                return true;
            }
        }
    }
}
