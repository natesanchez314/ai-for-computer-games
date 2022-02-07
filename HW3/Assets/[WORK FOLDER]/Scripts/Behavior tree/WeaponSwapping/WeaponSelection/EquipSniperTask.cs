class EquipSniperTask : Task
{ 
    public EquipSniperTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        survivor.SwitchWeapon(WEAPON_TYPE.SNIPER);
        return true;
    }
}
