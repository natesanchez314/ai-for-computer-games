using System;

class LootTask : Task
{
    public LootTask()
        : base()
    { }

    protected override void InitChildren()
    {
        AddTask(new GetLoot());
        AddTask(new TargetClosestTask());
        AddTask(new WeaponsTask());
    }
}
