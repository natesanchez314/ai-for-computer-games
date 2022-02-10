using UnityEngine;

class WeaponSwappingTask : SelectorTask // Change to selector task later
{
    public WeaponSwappingTask() : base()
    { }

    protected override void InitChildren()
    {
        AddTask(new EquipShotgunTask());
        AddTask(new EquipGrenadeLauncherTask());
        AddTask(new EquipAssaultTask());
        AddTask(new EquipSniperTask());
        AddTask(new EquipPistolTask());
        AddTask(new EquipNullTask());
    }
}
