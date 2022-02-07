class EquipShotgunTask : Task
{
    public EquipShotgunTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        survivor.SwitchWeapon(WEAPON_TYPE.SHOTGUN);
        return true;
    }
}
