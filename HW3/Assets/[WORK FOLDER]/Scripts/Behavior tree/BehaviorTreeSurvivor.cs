class BehaviorTreeSurvivor : Task
{
    public BehaviorTreeSurvivor() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        return base.Run(survivor);
    }

    protected override void InitChildren()
    {
        AddTask(new PriorityTargetingTask());
        AddTask(new MovementTask());
        AddTask(new WeaponsTask());
    }
}
