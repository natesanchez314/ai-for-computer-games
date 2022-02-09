using System.Collections.Generic;
using UnityEngine;

class UpdateClosestEnemyTask : Task
{
    public UpdateClosestEnemyTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        List<Enemy> visibleEnemies = survivor.blackboard.visibleEnemies;
        Enemy closestEnemy = null;
        float closestDist = 0.0f;
        foreach (Enemy enemy in visibleEnemies)
        {
            Vector3 survivorPos = survivor.transform.position;
            Vector3 enemyPos = survivor.transform.position;
            Vector3 dir = enemyPos - survivorPos;
            float dist = dir.magnitude;
            if (closestEnemy == null)
            {
                closestEnemy = enemy;
                closestDist = dist;
            }
            else if (dist < closestDist)
            {
                closestEnemy = enemy;
                closestDist = dist;
            }
        }
        survivor.blackboard.closestEnemy = closestEnemy;
        survivor.blackboard.priorityTarget = closestEnemy;
        if (closestEnemy == null)
            return false;
        return true;
    }
}
