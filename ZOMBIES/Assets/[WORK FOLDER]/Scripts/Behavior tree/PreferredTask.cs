using System;
using UnityEngine;

class PreferredTask : Task
{
    public PreferredTask()
        : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        return false;// GameObject.FindObjectOfType<HiveMind>().PreferredTask(survivor);
    }
}
