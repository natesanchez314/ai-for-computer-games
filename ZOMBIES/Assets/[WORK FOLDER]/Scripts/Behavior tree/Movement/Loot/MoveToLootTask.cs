using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MoveToLootTask : Task
{
    public MoveToLootTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (survivor.blackboard.closestEnemy == null)
        {
            Loot loot = survivor.blackboard.closestLoot;
            if (loot != null)
            {
                Debug.Log("Moving to loot");
                survivor.MoveTo(loot.transform.position);
                return true;
            }

            //Vector3 heading = survivor.transform.position - survivor.blackboard.closestLoot.transform.position;
        }
        return false;
    }
}
