using System;

class KiteTask : Task
{
    public KiteTask()
        : base()
    { }

    protected override void InitChildren()
    {
        AddTask(new TargetClosestTask());
        //AddTask(new AvoidZombiesTask());
        AddTask(new FindFarthestSafeNodeTask());
        AddTask(new MoveToTargetTask());
        //
        ////AddTask(new AvoidZombiesTask());
        AddTask(new WeaponsTask());
    }
}
