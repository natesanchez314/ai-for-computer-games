using UnityEngine;
using System.Collections;

class WeaponsTask : Task
{
    public WeaponsTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        return base.Run(survivor);
    }

    protected override void InitChildren()
    {
        //Debug.Log("here");
        AddTask(new DetermineBestWeaponTask());
        AddTask(new WeaponSwappingTask());
        AddTask(new FireWeaponTask());
    }
}