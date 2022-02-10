using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Wander : Task
{
    public Wander() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        Vector3 loot = survivor.blackboard.closestLoot.transform.position;
        if (loot != null)
        {
            survivor.MoveTo(loot);
            return true;
        }
        return false;
    }
}
