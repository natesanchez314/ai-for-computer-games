using UnityEngine;

class EquipAssaultTask : Task
{
    public EquipAssaultTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (GameManager.GetAmmo(WEAPON_TYPE.ASSAULT) > 0 && survivor.blackboard.preferredWeapon == WEAPON_TYPE.ASSAULT)
        {
            survivor.SwitchWeapon(WEAPON_TYPE.ASSAULT);
            return true;
        }
        return false;
    }
}
