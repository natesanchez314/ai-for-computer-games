using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class HiveMind : MonoBehaviour
{
    private Task lootTask;
    private Task huntTask;
    private Task defendTask;
    private Task kiteTask;
    private Task preferredTask;

    private Survivor_AI lootRunner; // The priority survivor to get loot while the others fight
    private Blackboard_Survivor.TaskType preferredTaskType;

    void Start()
    {
        lootTask = new LootTask();
        defendTask = new DefendTask();
        huntTask = new HuntTask();
        kiteTask = new KiteTask();
        preferredTask = defendTask;
        preferredTaskType = Blackboard_Survivor.TaskType.DEFEND;
    }

    void Update()
    {
        PathFinder.FindOverrunNodes();
        SetPreferredTask();
        AssignTasks();
    }

    /*public bool PreferredTask(Survivor_AI survivor)
    {
        return preferredTask.Run(survivor);
    }*/

    public void SetPreferredTask()
    {
        if (GameManager.getAllEnemies().Count == 0)
        {
            if (GameManager.GetLootList().Count == 0)
            {
                preferredTask = defendTask;
                preferredTaskType = Blackboard_Survivor.TaskType.DEFEND;
            }
            else
            {
                preferredTask = lootTask;
                preferredTaskType = Blackboard_Survivor.TaskType.LOOT;
            }
        }
        else
        {
            Survivor_AI[] survivors = GameObject.FindObjectsOfType<Survivor_AI>();
            if (survivors.Length < 2)
            {
                preferredTask = kiteTask;
                preferredTaskType = Blackboard_Survivor.TaskType.KITE;
            }
            else
            {
                preferredTask = defendTask;
                preferredTaskType = Blackboard_Survivor.TaskType.DEFEND;
            }
        }
    }

    private void AssignTasks()
    {
        Survivor_AI[] survivors = GameObject.FindObjectsOfType<Survivor_AI>();
        foreach(Survivor_AI survivor in survivors)
        {
            if (GameManager.getAllEnemies().Count > 0)
            {
                if (lootRunner == null)
                {
                    lootRunner = survivor;
                    survivor.AssignTask(lootTask, Blackboard_Survivor.TaskType.LOOT);
                }
                else if (survivor != lootRunner)
                    survivor.AssignTask(preferredTask,  preferredTaskType);
                else
                {
                    if (GameManager.GetLootList().Count == 0)
                        survivor.AssignTask(preferredTask, preferredTaskType);
                    else
                        survivor.AssignTask(lootTask, Blackboard_Survivor.TaskType.LOOT);
                }
            }
            else
            {
                if (GameManager.GetLootList().Count == 0)
                    survivor.AssignTask(preferredTask, preferredTaskType);
                else if (survivor == lootRunner)
                    survivor.AssignTask(lootTask, Blackboard_Survivor.TaskType.LOOT);
            }
            //survivor.AssignTask(kiteTask, Blackboard_Survivor.TaskType.KITE);
        }
    }
}
