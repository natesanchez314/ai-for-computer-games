using UnityEngine;

class EquipShotgunTask : Task
{
    public EquipShotgunTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        Debug.Log("    Equipping shotgun");
        survivor.SwitchWeapon(WEAPON_TYPE.SHOTGUN);
        return true;
    }
}
