using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AvoidZombiesTask : Task
{
    public override bool Run(Survivor_AI survivor)
    {
        Enemy closestZombie = survivor.blackboard.closestEnemy;
        if (closestZombie != null)
        {
            Vector3 survivorPos = survivor.transform.position;
            Vector3 zombiePos = closestZombie.transform.position;

            Vector3 heading = zombiePos - survivorPos;
            heading = survivorPos - heading;

            survivor.MoveTo(heading);
            return true;
        }
        return false;
    }
}
