using UnityEngine;

class EquipSniperTask : Task
{ 
    public EquipSniperTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (GameManager.GetAmmo(WEAPON_TYPE.SNIPER) > 0 && survivor.blackboard.preferredWeapon == WEAPON_TYPE.SNIPER)
        {
            survivor.SwitchWeapon(WEAPON_TYPE.SNIPER);
            return true;
        }
        return false;
    }
}
