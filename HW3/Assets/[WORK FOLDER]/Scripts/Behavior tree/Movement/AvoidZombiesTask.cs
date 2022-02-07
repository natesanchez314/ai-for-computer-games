using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AvoidZombiesTask : Task
{
    public override bool Run(Survivor_AI survivor)
    {
        //Debug.Log("Avoid!");
        Enemy closestZombie = Blackboard_Survivor.GetClosestEnemy(survivor);
        if (closestZombie != null)
        {
            //Debug.Log("Tim");
            Vector3 survivorPos = survivor.transform.position;
            Vector3 zombiePos = closestZombie.transform.position;

            Vector3 heading = survivorPos - zombiePos;
           // Vector3 desiredVelocity = heading.normalized * survivor.GetComponent<

            survivor.MoveTo(new Vector3(100, 38.14064f, 0));
            //Debug.Log("Butt");
            return true;
        }
        //Debug.Log("Closest is null");
        return false;
    }
}
