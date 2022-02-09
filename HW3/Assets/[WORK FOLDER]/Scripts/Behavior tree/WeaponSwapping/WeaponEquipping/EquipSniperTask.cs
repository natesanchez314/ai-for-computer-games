using UnityEngine;

class EquipSniperTask : Task
{ 
    public EquipSniperTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        Debug.Log("    Equipping sniper rifle");
        survivor.SwitchWeapon(WEAPON_TYPE.SNIPER);
        return true;
    }
}
