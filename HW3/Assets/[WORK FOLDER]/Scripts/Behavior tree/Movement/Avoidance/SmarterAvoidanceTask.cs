using UnityEngine;

class SmarterAvoidanceTask : Task
{
    public SmarterAvoidanceTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        Debug.Log("Smarter avoidance");
        return base.Run(survivor);
    }
}
