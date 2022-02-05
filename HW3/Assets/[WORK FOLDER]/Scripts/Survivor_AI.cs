using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor_AI : MonoBehaviour
{
    Survivor survivor;

    //Blackboard_Player blackboard;

    //Task BT_Basic;



    // Start is called before the first frame update
    void Start()
    {
        survivor = GetComponent<Survivor>();

        //blackboard = new Blackboard_Player();


    }

    // Update is called once per frame
    void Update()
    {
        //if (BT_Basic != null)
        //{
        //    BT_Basic.Run(this);
        //}
    }

    //public Blackboard_Player GetBlackboard()
    //{
    //    return blackboard;
    //}

    public void MoveTo(Vector3 target)
    {
        survivor.MoveTo(target);
    }

    public void Fire(Vector3 target)
    {
        survivor.Fire(target);
    }

    public Weapon GetCurrentWeapon()
    {
        return survivor.GetCurrentWeapon();
    }

    public void SwitchWeapon(WEAPON_TYPE newWeapon)
    {
        survivor.SwitchWeapons(newWeapon);
    }
}
