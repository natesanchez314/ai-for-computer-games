using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SetNewWanderHeadingTask : Task
{
    public SetNewWanderHeadingTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        /*Vector3 survivorPos = survivor.transform.position;
        Vector3 heading = survivor.blackboard.randomHeading;
        float xDiff = Mathf.Abs(survivorPos.x - heading.x);
        float zDiff = Mathf.Abs(survivorPos.z - heading.z);
        if (xDiff < 3.0f && zDiff < 3.0f)
        {
            survivor.blackboard.randomHeading = new Vector3(
                Random.Range(-50, 50),
                0.0f,
                Random.Range(-50, 50)
            );
            return true;
        }
        return false;*/

        if (survivor.blackboard.targetPathNode == null)
        {
            survivor.blackboard.targetPathNode = PathFinder.FindClosestNodeToTarget(survivor.transform.position);
            survivor.blackboard.lastVisited = survivor.blackboard.targetPathNode;
            return true;
        }
        else
        {
            Vector3 survivorPos = survivor.transform.position;
            Vector3 heading = survivor.blackboard.targetPathNode.GetPosition();
            float xDiff = Mathf.Abs(survivorPos.x - heading.x);
            float zDiff = Mathf.Abs(survivorPos.z - heading.z);
            if (xDiff < 1.0f && zDiff < 1.0f)
            {
                List<PathNode> potentialTargets = survivor.blackboard.targetPathNode.neighbors;
                if (potentialTargets.Count == 0 || potentialTargets.Count == 1)
                    survivor.blackboard.targetPathNode = survivor.blackboard.lastVisited;
                else
                {
                    PathNode newTarget = survivor.blackboard.targetPathNode;
                    while (newTarget == survivor.blackboard.lastVisited)
                    {
                        int randIndex = Random.Range(0, potentialTargets.Count - 1);
                        newTarget = potentialTargets[randIndex];
                    }
                    survivor.blackboard.targetPathNode = newTarget;
                }
                survivor.blackboard.lastVisited = survivor.blackboard.targetPathNode;
                return true;
            }
        }
        return false;
    }
}
