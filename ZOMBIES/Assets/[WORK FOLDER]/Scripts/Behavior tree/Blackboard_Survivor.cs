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
    
    public float priorityTargetDist;
    public float closestEnemyDist;
    public float closestLootDist;
    public WEAPON_TYPE preferredWeapon;

    public Dictionary<WEAPON_TYPE, float> weaponRanges;

    public PathNode targetPathNode;
    public PathNode lastVisited;
    public List<PathNode> path;
    public bool needsPath;

    public Vector3 heading;
    public Vector3 randomHeading;

    public Blackboard_Survivor()
    {
        visibleEnemies = new List<Enemy>();
        priorityTarget = null;
        closestEnemy = null;
        closestLoot = null;

        priorityTargetDist = 0.0f;
        closestEnemyDist = 0.0f;
        closestLootDist = 0.0f;

        weaponRanges = new Dictionary<WEAPON_TYPE, float>();
        preferredWeapon = WEAPON_TYPE.PISTOL;

        heading = Vector3.zero;
        heading = Vector3.zero;
        needsPath = true;
    }

    public void SetClosestEnemy(Enemy enemy, float dist)
    {
        closestEnemy = enemy;
        closestEnemyDist = dist;
    }

    public void SetPriorityTarget(Enemy enemy, float dist)
    {
        priorityTarget = enemy;
        priorityTargetDist = dist;
    }

    public void SetClosestLoot(Loot loot, float dist)
    {
        closestLoot = loot;
        closestLootDist = dist;
    }

    public float GetWeaponRange(WEAPON_TYPE type, Survivor_AI survivor)
    {
        if (!weaponRanges.ContainsKey(type))
            InitWeaponRange(type, survivor);
        return weaponRanges[type];
    }

    private void InitWeaponRange(WEAPON_TYPE type, Survivor_AI survivor)
    {
        Weapon[] weapons = survivor.GetWeaponList();
        foreach (Weapon weapon in weapons)
        {
            switch (weapon.type)
            {
                case WEAPON_TYPE.ASSAULT:
                    weaponRanges.Add(WEAPON_TYPE.ASSAULT, weapon.getRange());
                    break;
                case WEAPON_TYPE.GRENADE_LAUNCHER:
                    weaponRanges.Add(WEAPON_TYPE.GRENADE_LAUNCHER, weapon.getRange());
                    break;
                case WEAPON_TYPE.SHOTGUN:
                    weaponRanges.Add(WEAPON_TYPE.SHOTGUN, weapon.getRange());
                    break;
                case WEAPON_TYPE.SNIPER:
                    weaponRanges.Add(WEAPON_TYPE.SNIPER, weapon.getRange());
                    break;
                case WEAPON_TYPE.PISTOL:
                    weaponRanges.Add(WEAPON_TYPE.PISTOL, weapon.getRange());
                    break;
            }
        }
    }
}
