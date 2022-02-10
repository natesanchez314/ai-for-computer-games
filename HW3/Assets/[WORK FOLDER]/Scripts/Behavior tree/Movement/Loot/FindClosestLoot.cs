using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FindClosestLoot : Task
{
    public FindClosestLoot() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        List<Loot> loot = GameManager.GetLootList();
        Loot closestLoot = null;
        float closestDist = 0.0f;
        foreach (Loot l in loot)
        {
            Vector3 survivorPos = survivor.transform.position;
            Vector3 enemyPos = survivor.transform.position;
            Vector3 dir = enemyPos - survivorPos;
            float dist = dir.magnitude;
            if (closestLoot == null)
            {
                closestLoot = l;
                closestDist = dist;
            }
            else if (dist < closestDist)
            {
                closestLoot = l;
                closestDist = dist;
            }
        }
        survivor.blackboard.closestLoot = closestLoot;
        if (closestLoot == null)
            return false;
        return true;
    }
}
