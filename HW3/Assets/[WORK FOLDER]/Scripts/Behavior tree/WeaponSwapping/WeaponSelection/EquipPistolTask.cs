class EquipPistolTak : Task
{
    public EquipPistolTak() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        survivor.SwitchWeapon(WEAPON_TYPE.PISTOL);
        return true;
    }
}
