using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MoveToLootTask : SelectorTask
{
    public MoveToLootTask() : base()
    { }

    protected override void InitChildren()
    {
        this.children.Add(new BuildPathTask());
        this.children.Add(new FollowPathTask());
    }
}
