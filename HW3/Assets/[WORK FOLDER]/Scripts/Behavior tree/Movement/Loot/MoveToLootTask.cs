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
                survivor.MoveTo(loot.transform.position);
                return true;
            }
        }
        return false;
    }
}
