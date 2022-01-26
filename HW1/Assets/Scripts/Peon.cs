using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PEON_STATE
{
	AT_CASTLE,
	MOVING_TO_MINE,
    MOVING_TO_CASTLE,
    AT_MINE,
    MOVING_TO_FOREST,
    AT_FOREST,
    ENTER_MINE,
    DIGGING_FOR_GOLD,
    EXIT_MINE,
    OTHER

}

public class Peon : MonoBehaviour {

	// Simulating an arbitrary distance
	public int DISTANCE_TO_CASTLE = 10;
	public int DISTANCE_TO_MINE = 10;


	private static int PEON_ID_COUNTER = 0;

	public StateMachine FSM_Peon;

    Castle castleRef;
    Mine mineRef;
    Forest forestRef;
    GameController gameControllerRef;

    public float moveSpeed;
	public int health;
	public int attack;
	public int heldGold;
    public int heldWood;

	public int maxGold;
    public int maxWood;
	private int id;

    public bool preferGold;

	// Use this for initialization
	public void Start()
	{
		FSM_Peon = new StateMachine();

        FSM_Peon.AddState("At_Castle", Peon_AtCastle.Instance());
        FSM_Peon.AddState("At_Mine", Peon_AtMine.Instance());
        FSM_Peon.AddState("Moving_To_Castle", Peon_Moving_To_Castle.Instance());
        FSM_Peon.AddState("Moving_To_Mine", Peon_Moving_To_Mine.Instance());
        FSM_Peon.AddState("Moving_To_Forest", Peon_Moving_To_Forest.Instance());
        FSM_Peon.AddState("At_Forest", Peon_AtForest.Instance());

        FSM_Peon.ChangeState("At_Castle", this);

		id = ++PEON_ID_COUNTER;
		health = 10;
		attack = 0;
		heldGold = 0;
        heldWood = 0;
		maxGold = 100;
        maxWood = 100;
        preferGold = true;

        castleRef = GameObject.FindGameObjectWithTag("Castle").GetComponent<Castle>();
        mineRef = GameObject.FindGameObjectWithTag("Mine").GetComponent<Mine>();
        forestRef = GameObject.FindGameObjectWithTag("Forest").GetComponent<Forest>();
        gameControllerRef = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

	}

	public void Update()
	{
		FSM_Peon.Update(this);
	}

	public void PrintName()
	{
		Debug.Log("Peon " + id.ToString());
	}

    public bool MoveToMine()
    {
        return MoveTo(mineRef.gameObject.transform);
    }

    public bool MoveToCastle()
    {
        return MoveTo(castleRef.gameObject.transform);
    }

    public bool MoveToForest()
    {
        return MoveTo(forestRef.gameObject.transform);
    }

    private bool MoveTo(Transform target)
    {
        bool output = false;

        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        transform.position += direction * moveSpeed * Time.deltaTime;

        float distanceAway = Vector3.Distance(transform.position, target.position);

        if (distanceAway <= moveSpeed)
        {
            output = true;
            transform.position = target.position;
        }

        return output;
    }

    public void DropOffGold()
    {
        gameControllerRef.gold += heldGold;
        heldGold = 0;
    }

    public void DigForGold()
    {
        heldGold += 1;
    }

    public void ChopWood()
    {
        heldWood += 1;
    }

    // Use this function to allow you click between mining and chopping modes for a peon
    // This function is trigger if you mouse clicks the peon
    public void OnClick()
    {
        Debug.Log("Click");
        preferGold = !preferGold;
    }
}
