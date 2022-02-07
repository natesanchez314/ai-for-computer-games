class WeaponSwappingTask : RandomSelectorTask
{
    public WeaponSwappingTask() : base()
    { }

    public override bool Run(Survivor_AI survivor)
    {
        /* WEAPON_TYPE wt = ChooseBestWeapon();
         switch(wt)
         {
             case WEAPON_TYPE.PISTOL:
                 return 
         }*/
        return base.Run(survivor);
    }

    private WEAPON_TYPE ChooseBestWeapon()
    {
        return WEAPON_TYPE.NULL;
    }
}
