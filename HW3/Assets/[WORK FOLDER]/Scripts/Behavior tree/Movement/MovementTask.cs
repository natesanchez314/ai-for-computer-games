class MovementTask : SelectorTask
{
    public MovementTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {   
        if (children.Count == 0)
            InitChildren();
        return base.Run(survivor);
    }

    protected override void InitChildren()
    {
        AddTask(new AvoidZombiesTask());
        //AddTask(new GetLoot());
        AddTask(new WanderTask());
    }
}
