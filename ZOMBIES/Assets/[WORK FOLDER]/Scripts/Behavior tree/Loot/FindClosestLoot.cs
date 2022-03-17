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
            Vector3 lootPos = l.transform.position;
            Vector3 dir = lootPos - survivorPos;
            float dist = dir.magnitude;
            if (closestLoot == null || dist < closestDist)
            {
                closestLoot = l;
                closestDist = dist;
            }
        }
        if (closestLoot != null)
        {
            survivor.blackboard.SetClosestLoot(closestLoot, closestDist);
            survivor.blackboard.SetTarget(closestLoot.transform.position);
            return true;
        }
        return false;
    }
}
