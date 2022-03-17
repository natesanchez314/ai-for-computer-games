using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GetLoot : SequenceTask
{
    public GetLoot() : base()
    { }

    protected override void InitChildren()
    {
        AddTask(new FindClosestLoot());
        AddTask(new MoveToTargetTask());
        //AddTask(new PickUpLootTask());
    }
}
