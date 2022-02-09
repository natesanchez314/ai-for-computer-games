using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

class Blackboard_Survivor : Task
{
    public List<Enemy> visibleEnemies;
    public Enemy priorityTarget;
    public Enemy closestEnemy;
    public Loot closestLoot;

    public Blackboard_Survivor()
    {
        visibleEnemies = new List<Enemy>();
        priorityTarget = null;
        closestEnemy = null;
        closestLoot = null;
    }
}
