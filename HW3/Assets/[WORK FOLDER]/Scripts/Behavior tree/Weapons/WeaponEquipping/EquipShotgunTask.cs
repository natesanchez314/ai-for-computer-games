using UnityEngine;

class EquipShotgunTask : Task
{
    public EquipShotgunTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (GameManager.GetAmmo(WEAPON_TYPE.SHOTGUN) > 0 && survivor.blackboard.preferredWeapon == WEAPON_TYPE.SHOTGUN)
        {
            survivor.SwitchWeapon(WEAPON_TYPE.SHOTGUN);
            return true;
        }
        return false;
    }
}
