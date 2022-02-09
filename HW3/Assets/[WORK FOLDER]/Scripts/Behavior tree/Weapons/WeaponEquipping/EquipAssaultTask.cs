using UnityEngine;

class EquipAssaultTask : Task
{
    public EquipAssaultTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        Debug.Log("    Equipping assault rifle");
        survivor.SwitchWeapon(WEAPON_TYPE.ASSAULT);
        return true;
    }
}
