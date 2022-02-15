using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Task
{
    protected List<Task> children;

    public Task()
    {
        children = new List<Task>();
    }

    public virtual bool Run(Survivor_AI survivor)
    {
        if (children.Count == 0)
            InitChildren();
        foreach (Task child in children)
            child.Run(survivor);
        return true;
    }

    protected virtual void InitChildren()
    { }

    public void AddTask(Task task)
    {
        children.Add(task);
    }

    public void RemoveTask(Task task)
    {
        children.Remove(task);
    }

    protected void Shuffle()
    {
        List<Task> newChildren = new List<Task>();
        while (children.Count > 0)
        {
            int index = Random.Range(0, children.Count - 1);
            Task child = children[index];
            children.RemoveAt(index);
            newChildren.Add(child);
        }
        children = newChildren;
    }
}

class SelectorTask : Task
{
    public SelectorTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (children.Count == 0)
            InitChildren();
        foreach (Task child in children)
        {
            if (child.Run(survivor))
                return true;
        }
        return false;
    }
}

class SequenceTask : Task
{
    public SequenceTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (children.Count == 0)
            InitChildren();
        foreach (Task child in children)
        {
            if (!child.Run(survivor))
                return false;
        }
        return true;
    }
}

class RandomSelectorTask : Task
{
    public RandomSelectorTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (children.Count == 0)
            InitChildren();
        if (children.Count > 0)
        {
            int index = Random.Range(0, children.Count - 1);
            return children[index].Run(survivor);
        }
        return false;
    }
}

class NonDeterministicSelectorTask : SelectorTask
{ 
    public NonDeterministicSelectorTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (children.Count == 0)
            InitChildren();
        Shuffle();
        return base.Run(survivor);
    }
}

class NonDeterministicSequenceTask : SequenceTask
{ 
    public NonDeterministicSequenceTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (children.Count == 0)
            InitChildren();
        Shuffle();
        return base.Run(survivor);
    }
}