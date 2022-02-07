using UnityEngine;

class PriorityTargetingTask : Task
{
    public PriorityTargetingTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        Debug.Log("Targeting");
        return base.Run(survivor);
    }

    protected override void InitChildren()
    {
        AddTask(new UpdateTargetTask());
    }
}
