using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

class Blackboard_Survivor : Task
{
    private static Blackboard_Survivor instance;

    private Dictionary<Survivor_AI, Enemy> closestEnemies;

    private Blackboard_Survivor()
    {
        closestEnemies = new Dictionary<Survivor_AI, Enemy>();
    }

    public static List<Enemy> GetEnemies()
    {
        return GameManager.getAllEnemies();
    }

    public static List<Survivor> GetSurvivors()
    {
        return GameManager.getSurvivorList();
    }

    public static List<Loot> GetLoot()
    {
        return GameManager.GetLootList();
    }

    public static void UpdateClosestEnemy(Survivor_AI survivor, Enemy enemy)
    {
        if (instance == null)
            instance = new Blackboard_Survivor();
        if (!instance.closestEnemies.ContainsKey(survivor))
            instance.closestEnemies.Add(survivor, enemy);
        else
            instance.closestEnemies[survivor] = enemy;
    }

    public static Enemy GetClosestEnemy(Survivor_AI survivor)
    {
        if (instance == null)
            instance = new Blackboard_Survivor();
        if (instance.closestEnemies.ContainsKey(survivor))
            return instance.closestEnemies[survivor];
        else
            return null;
    }
}
