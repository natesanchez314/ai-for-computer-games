class EquipNullTask : Task
{
    public EquipNullTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        survivor.SwitchWeapon(WEAPON_TYPE.NULL);
        return true;
    }
}
