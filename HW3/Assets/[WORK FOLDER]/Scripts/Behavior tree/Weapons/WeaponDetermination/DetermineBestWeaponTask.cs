using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DetermineBestWeaponTask : Task
{
    public DetermineBestWeaponTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        Enemy enemy = survivor.blackboard.priorityTarget;
        //EnemyType type = enemy.GetEnemyType();
        if (enemy != null)
        {
            float enemyDist = survivor.blackboard.closestEnemyDist;

            if (enemyDist < survivor.blackboard.GetWeaponRange(WEAPON_TYPE.SHOTGUN, survivor))
                survivor.blackboard.preferredWeapon = WEAPON_TYPE.SHOTGUN;
            else if (enemyDist < survivor.blackboard.GetWeaponRange(WEAPON_TYPE.ASSAULT, survivor))
                survivor.blackboard.preferredWeapon = WEAPON_TYPE.ASSAULT;
            else if (enemyDist < survivor.blackboard.GetWeaponRange(WEAPON_TYPE.GRENADE_LAUNCHER, survivor))
                survivor.blackboard.preferredWeapon = WEAPON_TYPE.GRENADE_LAUNCHER;
            else if (enemyDist < survivor.blackboard.GetWeaponRange(WEAPON_TYPE.SNIPER, survivor))
                survivor.blackboard.preferredWeapon = WEAPON_TYPE.SNIPER;
            else
                survivor.blackboard.preferredWeapon = WEAPON_TYPE.PISTOL;
        }
        return true;
    }
}
