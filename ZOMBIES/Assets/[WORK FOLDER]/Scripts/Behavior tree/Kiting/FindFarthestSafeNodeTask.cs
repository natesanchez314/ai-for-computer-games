using System;
using UnityEngine;

class FindFarthestSafeNodeTask : Task
{
    public FindFarthestSafeNodeTask()
        : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (survivor.blackboard.closestEnemy == null)
            return false;
        PathNode[] pathNodes = PathFinder.GetPathNodes();
        PathNode farthestNode = null;
        float farthestDist = 0.0f;
        foreach (PathNode pathNode in pathNodes)
        {
            if (!pathNode.isDeadEnd)
            {
                Vector3 enemyPos = survivor.blackboard.closestEnemy.transform.position;
                Vector3 nodePos = pathNode.transform.position;
                Vector3 dir = nodePos - enemyPos;
                float dist = dir.magnitude;

                if (farthestNode == null)
                {
                    farthestNode = pathNode;
                    farthestDist = dist;
                }
                else if (dist > farthestDist)
                {
                    farthestNode = pathNode;
                    farthestDist = dist;
                }
            }
        }
        if (farthestNode != null)
        {
            survivor.blackboard.SetTarget(farthestNode.transform.position);
            return true;
        }
        return false;
    }
}
