using System.Collections.Generic;
using UnityEngine;

class UpdateTargetTask : Task
{
    public UpdateTargetTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        List<Enemy> enemies = Blackboard_Survivor.GetEnemies();
        if (enemies.Count > 0)
        {
            Enemy closestEnemy = null;
            float closestDist = 0.0f;
            foreach (Enemy enemy in enemies)
            {
                if (closestEnemy == null)
                {
                    closestEnemy = enemy;
                    closestDist = (survivor.transform.position - enemy.transform.position).magnitude;
                }
                else
                {
                    float dist = (survivor.transform.position - enemy.transform.position).magnitude;
                    if (dist < closestDist)
                    {
                        closestEnemy = enemy;
                        closestDist = dist;
                    }
                }
            }
            Blackboard_Survivor.UpdateClosestEnemy(survivor, closestEnemy);
            return true;
        }
        return false;
    }
}
