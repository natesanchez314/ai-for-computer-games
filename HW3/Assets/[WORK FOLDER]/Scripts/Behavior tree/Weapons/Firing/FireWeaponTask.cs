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
            Vector3 targetPos = target.transform.position;
            Vector3 surviverPos = survivor.transform.position;
            float dist = (targetPos - surviverPos).magnitude;

            if (dist < survivor.GetCurrentWeapon().getRange())
            {
                if (target.state == EnemyState.NORMAL)
                    survivor.Fire(targetPos);
                return true;
            }
        }
        return false;
    }
} 