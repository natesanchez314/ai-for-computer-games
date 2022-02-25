class MovementTask : SelectorTask
{
    public MovementTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {   
        return base.Run(survivor);
    }

    protected override void InitChildren()
    {
        //AddTask(new AvoidZombiesTask());
        AddTask(new GetLoot());  // Path finding
        // AddTask(); // Steering
        AddTask(new WanderTask());
    }
}
