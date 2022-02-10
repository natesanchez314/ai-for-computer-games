using System.Collections.Generic;
using UnityEngine;

class AvoidOthersTask : Task
{
    public AvoidOthersTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        List<Enemy> enemies = survivor.blackboard.visibleEnemies;
        foreach (Enemy enemy in enemies)
        {
            Vector3 survivorPos = survivor.transform.position;
            Vector3 enemyPos = survivor.transform.position;

            Vector3 heading = enemyPos - survivorPos;
            heading = heading.normalized;
            heading *= survivor.GetComponent<UnityEngine.AI.NavMeshAgent>().speed / 2.0f;
            heading = survivorPos - heading;

            survivor.blackboard.heading += heading;
            return true;
        }
        return false;
    }
}
