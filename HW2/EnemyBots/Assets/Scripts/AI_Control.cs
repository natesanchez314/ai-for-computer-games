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
	List<Collider2D> collisions = new List<Collider2D>();
	Vector3 wanderDisplacement = new Vector3(1, 0);
	Vector2 vSteer = Vector2.zero;

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

		ObstacleAvoidance();

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
		Vector2 hTarget = target - this.transform.position;
		Vector2 vDesired = hTarget.normalized * maxSpeed;
		
		Vector2 vSteering = vDesired - body.velocity;
		vSteering = Vector2.ClampMagnitude(vSteering, maxForce);

		body.velocity += vSteering;

		if (isDebugOn)
		{
			debugTarget.SetTarget(target);
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void Flee(Vector3 target)
	{
		Vector2 hTarget = this.transform.position - target;
		Vector2 vDesired = hTarget.normalized * maxSpeed;

		Vector2 vSteering = vDesired - body.velocity;
		vSteering = Vector2.ClampMagnitude(vSteering, maxForce);

		body.velocity += vSteering;

		if (isDebugOn)
		{
			debugTarget.SetTarget(target);
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void Arrive(Vector3 target)
	{
		Vector2 hTarget = target - this.transform.position;
		Vector2 vDesired = hTarget.normalized * maxSpeed;

		float distFromTarget = hTarget.magnitude;

		if (distFromTarget < minRadiusArrival)
        {
			vDesired *= (distFromTarget / minRadiusArrival);
        }

		Vector2 vSteering = vDesired - body.velocity;
		vSteering = Vector2.ClampMagnitude(vSteering, maxForce);

		body.velocity += vSteering;

		if (isDebugOn)
		{
			//debugTarget.SetTarget();  // what is our target during arrive?
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void Pursue()
	{
        Vector2 playerPos = player.getPosition2D();
        Vector2 playerVel = player.getVelocity();

		Vector2 target = playerPos + playerVel * (playerPos / maxSpeed);

		Vector2 hTarget = target - (Vector2)this.transform.position;
		Vector2 vDesired = hTarget.normalized * maxSpeed;

		Vector2 vSteering = vDesired - body.velocity;
		vSteering = Vector2.ClampMagnitude(vSteering, maxForce);

		body.velocity += vSteering;

		if (isDebugOn)
		{
			debugTarget.SetTarget(target);
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void Evade()
	{
		Vector2 playerPos = player.getPosition2D();
		Vector2 playerVel = player.getVelocity();

		Vector2 target = playerPos + playerVel * (playerPos / maxSpeed);

		Vector2 hTarget = (Vector2)this.transform.position - target;
		Vector2 vDesired = hTarget.normalized * maxSpeed;

		Vector2 vSteering = vDesired - body.velocity;
		vSteering = Vector2.ClampMagnitude(vSteering, maxForce);

		body.velocity += vSteering;

		if (isDebugOn)
		{
			debugTarget.SetTarget(target);
			debugVelocity.SetVelocity(body.velocity);
		}

		if (isDebugOn)
		{
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void Wander()
	{
		float CIRCLE_RADIUS = 1.0f;
		float CIRCLE_DIST = 3.0f;
		float ANGLE_CHANGE = 0.5f;

		//Vector3 circleCenter = this.body.transform.forward + (Vector3)this.body.position;
		Vector3 circleCenter = this.body.velocity;
		circleCenter = circleCenter.normalized;
		circleCenter *= CIRCLE_DIST;

		float angle = Random.Range(-15.0f, 15.0f);
		wanderDisplacement = Quaternion.Euler(0, 0, (angle * ANGLE_CHANGE) - (ANGLE_CHANGE * 0.5f)) * wanderDisplacement;
		wanderDisplacement = wanderDisplacement.normalized * CIRCLE_RADIUS;
		Vector3 target = circleCenter + wanderDisplacement;

        Vector2 hTarget = target - this.transform.position;
        Vector2 vDesired = hTarget.normalized * maxSpeed / 2;
        Vector2 vSteering = vDesired - body.velocity;
        vSteering = Vector2.ClampMagnitude(vSteering, maxForce);
        body.velocity += vSteering;
		vSteer = vSteering;

        if (isDebugOn)
		{
			debugCircle.setCircle(circleCenter, CIRCLE_RADIUS);
			debugVelocity.SetVelocity(body.velocity);
			debugTarget.SetTarget(target);
		}
	}

	void Hide()
	{
		// TODO: HIDE
		GameObject closestSphere = GetClosestSphere();
		if (closestSphere != null)
		{
			//Vector3 targetDir = closestSphere.transform.position + (Vector3)player.getPosition2D();
			//Vector3 hidingSpot = closestSphere.transform.position + targetDir.normalized * (closestSphere.GetComponent<CircleCollider2D>().radius);
			//Seek(hidingSpot);

			Vector3 target = Vector3.Project(closestSphere.transform.position, player.getPosition2D());
			Vector3 hidingSpot = (Vector3)closestSphere.transform.position + target.normalized * (closestSphere.GetComponent<CircleCollider2D>().radius + 2);

			Seek(hidingSpot);

			//if (isDebugOn)
			//{
			//	debugTarget.SetTarget(hidingSpot);
			//	debugCircle.setCircle(hidingSpot, 2.0f);
			//	debugVelocity.SetVelocity(body.velocity);
			//}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
    {
		//Debug.Log("Adding...");
		collisions.Add(col);
    }

	void OnTriggerExit2D(Collider2D col)
	{
		//Debug.Log("Removing...");
		collisions.Remove(col);
	}

	void ObstacleAvoidance()
	{
		// TODO: AVOIDANCE
		// just because it's not it's own state, doesn't mean we shouldn't also work on avoiding obstacles during all other states

		//SphereAvoidanceMapped();
		//SphereAvoidance();
		WallAvoidance();
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

	void SphereAvoidanceMapped()
	{
		CircleCollider2D closest = GetClosestSphere().GetComponent<CircleCollider2D>();
		if (closest != null)
		{
			Vector3 headingTarget = (Vector3)this.body.position - closest.transform.position;
			Vector3 desiredVelocity = headingTarget.normalized * maxSpeed * Vector3.Distance(this.body.position, closest.transform.position);
			Vector3 brakingForce = desiredVelocity - (Vector3)this.body.velocity;
			brakingForce = Vector2.ClampMagnitude(brakingForce, maxForce);

			Vector3 steeringForce = Vector3.Cross((Vector3)this.body.velocity, brakingForce);
			steeringForce = steeringForce.normalized * maxSpeed * closest.radius;
			//steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);

			this.body.velocity += (Vector2)(steeringForce + brakingForce);

		}
	}

	void SphereAvoidance()
    {
		CircleCollider2D closest = null;
		float closestDist = 0.0f;
		foreach (Collider2D collision in collisions)
		{
			if (collision != null && collision)
			{
				if (closest == null && collision.GetType() == typeof(CircleCollider2D))
				{
					closest = (CircleCollider2D)collision;
					closestDist = Vector3.Distance(this.gameObject.transform.position, collision.transform.position);
				}
				else
				{
					float dist = Vector3.Distance(this.gameObject.transform.position, collision.transform.position);
					if (dist < closestDist)
					{
						closest = (CircleCollider2D)collision;
						closestDist = dist;
					}
				}
			}
		}

		if (closest != null)
		{
			//Vector3 headingTarget = (Vector3)this.body.position - closest.transform.position;
			//Vector3 desiredVelocity = headingTarget.normalized * maxSpeed * closestDist;
			//Vector3 brakingForce = desiredVelocity - (Vector3)this.body.velocity;
			//brakingForce = Vector2.ClampMagnitude(brakingForce, maxForce);

			//Vector3 steeringForce = Vector3.Cross((Vector3)this.body.velocity, brakingForce);
			//steeringForce = steeringForce.normalized * maxSpeed * closest.radius;
			////steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);

			//this.body.velocity += (Vector2)(steeringForce + brakingForce);

			

			Vector3 avoidanceForce = (Vector3)this.body.velocity - closest.transform.position;
			avoidanceForce = avoidanceForce.normalized * maxForce;

			this.body.velocity += (Vector2)avoidanceForce;
		}

		if (isDebugOn)
		{
			if (closest != null)
				Debug.Log("Closest" + closest.name);
			debugVelocity.SetVelocity(body.velocity);
		}
	}		

	void WallAvoidance()
    {

    }

	GameObject GetClosestSphere()
	{ 
		GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");
		GameObject closest = null;
		float closestDist = 0.0f;
		foreach (GameObject collision in spheres)
		{
			if (closest == null)
			{
				closest = collision;
				closestDist = Vector3.Distance(this.gameObject.transform.position, collision.transform.position);
			}
			else
			{
				float dist = Vector3.Distance(this.gameObject.transform.position, collision.transform.position);
				if (dist < closestDist)
				{
					closest = collision;
					closestDist = dist;
				}
			}
		}
		Debug.Log("Closest: " + closest);
		return closest;
	}
}
