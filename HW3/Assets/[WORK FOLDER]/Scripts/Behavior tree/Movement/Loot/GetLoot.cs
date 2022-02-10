using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GetLoot : Task
{
    public GetLoot() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        //Debug.Log("Getting loot");
        return base.Run(survivor);
    }

    protected override void InitChildren()
    {
        AddTask(new FindClosestLoot());
        AddTask(new MoveToLootTask());
        AddTask(new PickUpLootTask());
    }
}
