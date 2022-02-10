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

    public float GetWeaponRange(WEAPON_TYPE type)
    {
        if (!weaponRanges.ContainsKey(type))
            InitWeaponRange(type);
        return weaponRanges[type];
    }

    private void InitWeaponRange(WEAPON_TYPE type)
    {
        Weapon weapon;
        switch (type)
        {
            case WEAPON_TYPE.PISTOL:
                weapon = new Pistol();
                weaponRanges.Add(type, weapon.getRange());
                break;
            case WEAPON_TYPE.ASSAULT:
                weapon = new AssaultRifle();
                weaponRanges.Add(type, weapon.getRange());
                break;
            case WEAPON_TYPE.SNIPER:
                weapon = new SniperRifle();
                weaponRanges.Add(type, weapon.getRange());
                break;
            case WEAPON_TYPE.SHOTGUN:
                weapon = new Shotgun();
                weaponRanges.Add(type, weapon.getRange());
                break;
            case WEAPON_TYPE.GRENADE_LAUNCHER:
                weapon = new GrenadeLauncher();
                weaponRanges.Add(type, weapon.getRange());
                break;
        }
    }
}
