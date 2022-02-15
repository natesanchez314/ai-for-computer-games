using UnityEngine;

class EquipGrenadeLauncherTask : Task
{
    // Start is called before the first frame update
    public EquipGrenadeLauncherTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        if (GameManager.GetAmmo(WEAPON_TYPE.GRENADE_LAUNCHER) > 0 && survivor.blackboard.preferredWeapon == WEAPON_TYPE.GRENADE_LAUNCHER)
        {
            survivor.SwitchWeapon(WEAPON_TYPE.GRENADE_LAUNCHER);
            return true;
        }
        return false;
    }
}
