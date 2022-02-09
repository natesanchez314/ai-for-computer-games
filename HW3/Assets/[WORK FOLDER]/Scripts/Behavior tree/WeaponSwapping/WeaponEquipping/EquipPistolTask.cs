using UnityEngine;

class EquipPistolTask : Task
{
    public EquipPistolTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        Debug.Log("    Equipping pistol");
        survivor.SwitchWeapon(WEAPON_TYPE.PISTOL);
        return true;
    }
}
