using UnityEngine;

class PriorityTargetingTask : Task
{
    public PriorityTargetingTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        return base.Run(survivor);
    }

    protected override void InitChildren()
    {
        AddTask(new LineOfSightTask());
        AddTask(new UpdateClosestEnemyTask());
        //AddTask(new TargetStrongZombieTask());
    }
}
