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
        survivor.blackboard.SetClosestLoot(closestLoot, closestDist);
        if (closestLoot == null)
        {
            Debug.Log("Did not find loot");
            return false;
        }
        Debug.Log("Found loot");
        return true;
    }
}
