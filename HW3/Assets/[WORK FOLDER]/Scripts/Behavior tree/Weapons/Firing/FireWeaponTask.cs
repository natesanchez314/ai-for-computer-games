using UnityEngine;
using System.Collections;

class FireWeaponTask : Task
{
    public FireWeaponTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        Enemy target = survivor.blackboard.priorityTarget;
        if (target != null)
        {
            survivor.Fire(survivor.blackboard.priorityTarget.transform.position);
            return true;
        }
        return false;
    }
} 