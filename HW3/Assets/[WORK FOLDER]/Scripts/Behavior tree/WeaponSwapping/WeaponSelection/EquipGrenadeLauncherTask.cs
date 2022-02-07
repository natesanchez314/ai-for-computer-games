class EquipGrenadeLauncherTask : Task
{
    // Start is called before the first frame update
    public EquipGrenadeLauncherTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        survivor.SwitchWeapon(WEAPON_TYPE.GRENADE_LAUNCHER);
        return true;
    }
}
