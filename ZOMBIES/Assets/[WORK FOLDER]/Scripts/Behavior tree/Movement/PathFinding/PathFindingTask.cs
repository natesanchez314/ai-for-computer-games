using System;


class PathFindingTask : SequenceTask
{
    public PathFindingTask()
        : base()
    { }

    protected override void InitChildren()
    {
        this.AddTask(new BuildPathTask());
        this.AddTask(new FollowPathTask());
    }
}
