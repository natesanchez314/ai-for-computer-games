using UnityEngine;
using System.Collections.Generic;

class LineOfSightTask : Task
{
    public LineOfSightTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        List<Enemy> enemies = GameManager.getAllEnemies();
        survivor.blackboard.visibleEnemies.Clear();
        RaycastHit hit;
        foreach (Enemy enemy in enemies)
        {
            //Debug.Log("Checking for enemies");
            Vector3 survivorPos = survivor.transform.position;
            Vector3 enemyPos = enemy.transform.position;
            Vector3 dir = enemyPos - survivorPos;

            if (Physics.Raycast(survivorPos, dir, out hit))
            {
                if (enemyPos == hit.transform.position)
                    survivor.blackboard.visibleEnemies.Add(enemy);
            }
        }
        //Debug.Log(enemies.Count);
        return base.Run(survivor);
    }
}
