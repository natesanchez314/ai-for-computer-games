using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BuildPathTask : Task
{
    public BuildPathTask()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        // If no path or path is blocked, create new path
        if (survivor.blackboard.visibleEnemies.Count > 0)
            survivor.blackboard.needsPath = true;
        if (survivor.blackboard.needsPath)
        {
            Vector3 survivorPos = survivor.transform.position;
            Vector3 target = survivor.blackboard.targetPos;

            if ((target - survivorPos).magnitude <= 2.0f)
            {
                survivor.blackboard.path = null;
                return false;
            }

            PathNode startNode = PathFinder.FindClosestNodeToTarget(survivorPos);
            PathNode endNode = PathFinder.FindClosestNodeToTarget(target);
            
            survivor.blackboard.path = PathFinder.FindPath(startNode, endNode);

            survivor.blackboard.needsPath = false;
            return true;
        }
        return false;
    }
}
