using System;
using System.Collections.Generic;
using UnityEngine;

class AvoidDeadEndTask : Task
{
    public AvoidDeadEndTask()
        : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (survivor.blackboard.closestEnemy != null)
        {
            List<PathNode> deadEnds = PathFinder.GetDeadEnds();
            foreach (PathNode deadEnd in deadEnds)
            {
                Vector3 survivorPos = survivor.transform.position;
                Vector3 enemyPos = deadEnd.transform.position;

                Vector3 heading = enemyPos - survivorPos;
                heading = heading.normalized;
                heading *= survivor.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
                heading = survivorPos - heading;

                survivor.blackboard.heading += heading;
                return true;
            }
        }
        return false;
    }
}
