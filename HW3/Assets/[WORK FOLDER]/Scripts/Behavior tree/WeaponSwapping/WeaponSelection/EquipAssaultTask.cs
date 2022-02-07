class EquipAssaultTask : Task
{
    public EquipAssaultTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        survivor.SwitchWeapon(WEAPON_TYPE.ASSAULT);
        return true;
    }
}
