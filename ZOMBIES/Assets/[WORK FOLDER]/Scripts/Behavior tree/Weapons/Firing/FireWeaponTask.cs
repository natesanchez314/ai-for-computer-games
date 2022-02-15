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
            if (survivor.blackboard.closestEnemyDist < survivor.GetCurrentWeapon().getRange())
            {
                if (target.state == EnemyState.NORMAL)
                    survivor.Fire(target.transform.position);
                return true;
            }
        }
        return false;
    }
} 