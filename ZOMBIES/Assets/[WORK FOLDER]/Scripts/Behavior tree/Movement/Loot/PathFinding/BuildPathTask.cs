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

        if (survivor.blackboard.needsPath)
        {
            Vector3 survivorPos = survivor.transform.position;
            Vector3 lootPos = survivor.blackboard.closestLoot.transform.position;

            PathNode startNode = PathFinder.FindClosestNodeToTarget(survivorPos);
            PathNode endNode = PathFinder.FindClosestNodeToTarget(lootPos);

            survivor.blackboard.path = PathFinder.FindPath(startNode, endNode);

            survivor.blackboard.needsPath = false;
            return true;
            /*if (survivor.blackboard.closestEnemy == null)
            {
                Loot loot = survivor.blackboard.closestLoot;
                if (loot != null)
                {
                    //Debug.Log("Moving to loot");
                    survivor.MoveTo(loot.transform.position);
                    return true;
                }

                //Vector3 heading = survivor.transform.position - survivor.blackboard.closestLoot.transform.position;
            }
            return false;
        }*/

        }
        return false;
    }
}
