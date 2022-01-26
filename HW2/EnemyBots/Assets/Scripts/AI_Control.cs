using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AI_STATE
{
	NULL,
	BROKEN,
	SEEK,
	FLEE,
	ARRIVE,
	PURSUE,
	EVADE,
	WANDER,
	HIDE,
	GROUP_BEHAVIOR
}


public class AI_Control : MonoBehaviour 
{
	// reference to the PlayerControl - this is our target!
	PlayerControl player;

	// reference to our own RigidBody component, which controls the physics of the AI Bot
	// for the purpose of this project, it is the way we access our velocity
	Rigidbody2D body;

	// our internal state system -- this is PUBLIC and can be changed in the Inspector window for easy demonstrating! 
	public AI_STATE state;

	// these are for our debug, don't worry about them
	DebugTarget debugTarget;
	DebugVelocity debugVelocity;
	DebugCircle debugCircle;

	// public movement variables - feel free to add more

	public bool isDebugOn = true;

	public float maxForce = 0.2f;
	public float maxSpeed = 10.0f;

	public float minRadiusArrival = 0.5f;




	// Initialization
	void Start () 
	{
		// gather references
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl>();
		body = GetComponent<Rigidbody2D> ();

		// set up debug
		debugTarget = GetComponentInChildren<DebugTarget> ();
		debugVelocity = GetComponentInChildren<DebugVelocity> ();
		debugCircle = GetComponentInChildren<DebugCircle> ();

		// start the bot off with a RANDOM DIRECTION at max velocity
		// --> this of course may be overriden later by state behavior or forces to the rigidbody
		body.velocity = Random.insideUnitCircle.normalized * maxSpeed;
	}


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
			isDebugOn = !isDebugOn;
        }
    }

    // FixedUpdate is like update, but it occurs in step with the physics calculations
    // We're using this since we're making asjustments to the RigidBody, which controls physics for this game object
    void FixedUpdate () 
	{
        // clear old debug lines

		debugCircle.Clear();
		debugTarget.Clear();
		debugVelocity.Clear();

		// switch statement to find new velocity based on the current state

		switch (state) 
		{
			case AI_STATE.BROKEN:
				BrokenBehavior();
				break;
			case AI_STATE.SEEK:
				Seek (player.getPosition2D());
				break;
			case AI_STATE.FLEE:
				Flee (player.getPosition2D());
				break;
			case AI_STATE.ARRIVE:
				Arrive (player.getPosition2D());
				break;
			case AI_STATE.PURSUE:
				Pursue ();
				break;
			case AI_STATE.EVADE:
				Evade ();
				break;
			case AI_STATE.WANDER:
				Wander ();
				break;
			case AI_STATE.HIDE:
				Hide ();
				break;
			case AI_STATE.GROUP_BEHAVIOR:
				GroupBehavior();
				break;
			default:
				break;
		}

		// just in case... clamp the velocity to our max velocity
		body.velocity = Vector3.ClampMagnitude(body.velocity, maxSpeed);

		// rotate the AI bot to match the new velocity

		float angle = Mathf.Atan2 (body.velocity.y, body.velocity.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}

	void BrokenBehavior()
    {
		// let's just define some random broken behavior, like rotating in place - that feels sufficiently broken to me.
		// To practice using a target destination, let's create an invisible target circling around the bot

		float radius = 15;
		float angle = Time.time * 5.0f;
		Vector3 circlingTarget = body.position + new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius); // (Don't worry about this math)

		// now that we have a target, let's find the direction toward that target from this bot's transform

		Vector2 dirTowardsTarget = new Vector2(circlingTarget.x, circlingTarget.y)
			- new Vector2(transform.position.x, transform.position.y);

		// we access the velocity of the bot through the rigidbody, which looks like --> body.velocity
		// since we're not trying to create realistic bot behavior, it's safe to just set the velocity to this new direction,
		// but of course let's normalize it and scale it by half the max velocity first

		body.velocity = dirTowardsTarget.normalized * maxSpeed * 0.5f;

		// now we can draw the velocity for debug purposes

		if (isDebugOn)
        {
			debugTarget.SetTarget(circlingTarget);
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void Seek(Vector3 target)
	{
		// TODO: SEEK




		if (isDebugOn)
		{
			debugTarget.SetTarget(target);
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void Flee(Vector3 target)
	{
		// TODO: FLEE




		if (isDebugOn)
		{
			debugTarget.SetTarget(target);
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void Arrive(Vector3 target)
	{
		// TODO: ARRIVE




		if (isDebugOn)
		{
			//debugTarget.SetTarget();  // what is our target during arrive?
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void Pursue()
	{
		// TODO: PURSUE
        Vector2 playerPos = player.getPosition2D();
        Vector2 playerVel = player.getVelocity();




		if (isDebugOn)
		{
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void Evade()
	{
		// TODO: EVADE




		if (isDebugOn)
		{
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void Wander()
	{
		// TODO: WANDER




		if (isDebugOn)
		{
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void Hide()
	{
		// TODO: HIDE




		if (isDebugOn)
		{
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void ObstacleAvoidance()
	{
		// TODO: AVOIDANCE
		// just because it's not it's own state, doesn't mean we shouldn't also work on avoiding obstacles during all other states




		if (isDebugOn)
		{
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void GroupBehavior()
	{
		// TODO: GROUP BEHAVIOR
		// rather than performing the logic here, individual bots will likely
		// get desired behavior from a controller class while in Group Behavior state




		if (isDebugOn)
		{
			debugVelocity.SetVelocity(body.velocity);
		}
	}
}
