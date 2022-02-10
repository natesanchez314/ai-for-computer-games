using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AvoidZombiesTask : SequenceTask
{
    public AvoidZombiesTask() : base()
    { }

    protected override void InitChildren()
    {
        AddTask(new AvoidMainTask());
        AddTask(new AvoidOthersTask());
        AddTask(new MoveTask());
    }
}
