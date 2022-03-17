using System;
using UnityEngine;

class SteerToLootTask : Task
{
    public SteerToLootTask()
        : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        Loot closestLoot = survivor.blackboard.closestLoot;
        if (closestLoot != null)
        {
            Debug.Log("Steering to loot");
            Vector3 survivorPos = survivor.transform.position;
            Vector3 lootPos = closestLoot.transform.position;

            Vector3 heading = survivorPos - lootPos;
            heading = heading.normalized;
            heading *= survivor.GetComponent<UnityEngine.AI.NavMeshAgent>().speed / 2.0f;

            heading = survivorPos - heading;

            survivor.blackboard.heading = heading;
            return true;
        }
        return false;
    }
}
