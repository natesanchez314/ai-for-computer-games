using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor_AI : MonoBehaviour
{
    Survivor survivor;

    //Blackboard_Survivor blackboard;

    Task BT_Basic;



    // Start is called before the first frame update
    void Start()
    {
        survivor = GetComponent<Survivor>();
        //BT_Basic = new BehaviorTreeSurvivor();
    }

    // Update is called once per frame
    void Update()
    {
        this.MoveTo(new Vector3(1000, 1000, 0));
        /*if (BT_Basic == null)
            BT_Basic = new BehaviorTreeSurvivor();
        BT_Basic.Run(this);*/
    }

    /*public Blackboard_Survivor GetBlackboard()
    {
        return blackboard;
    }*/

    public void MoveTo(Vector3 target)
    {
        Debug.Log("Pre move call");
        survivor.MoveTo(target);
        Debug.Log("Post move call");
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
