using System;
using System.Collections.Generic;
using UnityEngine;

class FindClosestDefensiveNodeTask : Task
{
    public FindClosestDefensiveNodeTask()
        : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (survivor.blackboard.needsNewDefensivePosition)
        {
            List<PathNode> defensivePositions = PathFinder.GetDefensivePositions();
            if (defensivePositions.Count == 0)
                return false;
            PathNode closestNode = null;
            float closestDist = 0.0f;
            foreach (PathNode pathNode in defensivePositions)
            {
                if (pathNode.GetSurvivor() == null || pathNode.GetSurvivor() == survivor)
                {
                    if (pathNode.overrun == false && pathNode != PathFinder.FindClosestNodeToTarget(survivor.transform.position))
                    {
                        Vector3 survivorPos = survivor.transform.position;
                        Vector3 nodePos = pathNode.transform.position;
                        Vector3 dir = nodePos - survivorPos;
                        float dist = dir.magnitude;
                        if (closestNode == null)
                        {
                            closestNode = pathNode;
                            closestDist = dist;
                            closestNode.SetSurvivor(survivor);
                        }
                        else if (dist < closestDist)
                        {
                            closestNode.SetSurvivor(null);
                            closestNode = pathNode;
                            closestDist = dist;
                            closestNode.SetSurvivor(survivor);
                        }
                    }
                }
            }
            if (closestNode != null)
            {
                survivor.blackboard.SetTarget(closestNode.transform.position);
                return true;
            }
            Debug.Log("No defensive positions found.");
        }
        return false;
    }
}

