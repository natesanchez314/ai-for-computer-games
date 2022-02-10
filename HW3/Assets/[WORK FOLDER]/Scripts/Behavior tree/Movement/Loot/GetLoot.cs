using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GetLoot : Task
{
    public GetLoot() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (children.Count == 0)
            InitChildren();
        return base.Run(survivor);
    }

    protected override void InitChildren()
    {
        AddTask(new FindClosestLoot());
        AddTask(new PickUpLootTask());
    }
}
