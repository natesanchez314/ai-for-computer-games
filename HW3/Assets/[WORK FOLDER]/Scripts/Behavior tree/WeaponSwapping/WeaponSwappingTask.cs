using UnityEngine;

class WeaponSwappingTask : RandomSelectorTask // Change to selector task later
{
    public WeaponSwappingTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        Debug.Log("Swapping weapons!");
        if (this.children.Count == 0)
            InitChildren();
        return base.Run(survivor);
    }

    protected override void InitChildren()
    {
        AddTask(new EquipAssaultTask());
        AddTask(new EquipGrenadeLauncherTask());
        AddTask(new EquipPistolTask());
        AddTask(new EquipShotgunTask());
        AddTask(new EquipSniperTask());
        //AddTask(new EquipNullTask());
    }
}
