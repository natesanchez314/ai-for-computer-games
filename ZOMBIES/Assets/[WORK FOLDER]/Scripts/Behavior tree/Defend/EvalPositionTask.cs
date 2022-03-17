using System;

class EvalPositionTask : Task
{
    public EvalPositionTask()
        : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        PathNode node = PathFinder.FindClosestNodeToTarget(survivor.transform.position);
        if (node.isDefensivePosition == false || node.overrun == true)
            survivor.blackboard.needsNewDefensivePosition = true;
        else if (survivor.blackboard.closestEnemy != null && survivor.blackboard.closestEnemyDist < 10)
            survivor.blackboard.needsNewDefensivePosition = true;
        else
            survivor.blackboard.needsNewDefensivePosition = false;
        return true;
    }
}