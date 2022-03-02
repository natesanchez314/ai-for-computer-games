using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AvoidMainTask : Task
{
    public AvoidMainTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        Enemy closestZombie = survivor.blackboard.closestEnemy;
        Debug.Log(closestZombie);
        if (closestZombie != null)
        {
            Debug.Log("Avoiding");
            Vector3 survivorPos = survivor.transform.position;
            Vector3 zombiePos = closestZombie.transform.position;

            Vector3 heading = zombiePos - survivorPos;
            heading = heading.normalized;
            heading *= survivor.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;

            heading = survivorPos - heading;

            survivor.blackboard.heading = heading;
            survivor.MoveTo(heading);
            return true;
        }
        //survivor.blackboard.heading = survivor.transform.position;
        return false;
    }
}
