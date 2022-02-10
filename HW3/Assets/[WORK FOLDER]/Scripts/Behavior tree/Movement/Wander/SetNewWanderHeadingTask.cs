using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SetNewWanderHeadingTask : Task
{
    public SetNewWanderHeadingTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        Vector3 survivorPos = survivor.transform.position;
        Vector3 heading = survivor.blackboard.randomHeading;
        float dist = (heading - survivorPos).magnitude;
        if (dist < 5.0f)
        {
            survivor.blackboard.randomHeading = new Vector3(
                Random.Range(-50, 50),
                0.0f,
                Random.Range(-50, 50)
            );
            return true;
        }
        return false;
    }
}
