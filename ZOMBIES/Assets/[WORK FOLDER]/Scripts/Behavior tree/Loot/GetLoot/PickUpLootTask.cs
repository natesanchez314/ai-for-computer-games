using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PickUpLootTask : Task
{
    public PickUpLootTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (survivor.blackboard.closestLoot != null)
        {
            if (survivor.blackboard.closestLootDist < 3.0f && survivor.blackboard.closestLootDist != 0.0f)
            {
                GameManager.PickupLoot(survivor.blackboard.closestLoot);
                Debug.Log("Picking up loot");
               //return true;
            }
        }
        return true;
    }
}
