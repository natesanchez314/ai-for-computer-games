using System;

class DefendTask : Task
{
    public DefendTask()
        : base()
    { }

    protected override void InitChildren()
    {
        AddTask(new EvalPositionTask());
        AddTask(new FindClosestDefensiveNodeTask());
        AddTask(new MoveToTargetTask());
        AddTask(new TargetClosestTask());
        AddTask(new WeaponsTask());
    }
}
