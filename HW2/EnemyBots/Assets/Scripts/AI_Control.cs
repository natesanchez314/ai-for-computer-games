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

	public float minRadiusArrival = 5.0f;
	public float avoidanceForce = 0.2f;
	public float idealDist = 2.0f;
	public float maxSeparationForce = 0.4f;
	public float maxCohesionForce = 0.4f;

	GameObject[] spheres;
	List<BoxCollider2D> wallCollisions = new List<BoxCollider2D>();
	List<CircleCollider2D> collisions = new List<CircleCollider2D>();
	GameObject[] enemies;
	Vector3 wanderDisplacement = new Vector3(1, 0);
	//Vector2 vSteer = Vector2.zero;

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

		spheres = GameObject.FindGameObjectsWithTag("Sphere");
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
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

		vDesired = Vector2.ClampMagnitude(vDesired, maxSpeed);

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
		Vector2 target = player.getPosition2D();
		Vector2 playerVel = player.getVelocity();

		Vector2 hTarget = target + playerVel * (target / maxSpeed);

		hTarget = hTarget - (Vector2)this.transform.position;
		Vector2 vDesired = hTarget.normalized * maxSpeed;

		Vector2 vSteering = vDesired - body.velocity;
		vSteering = Vector2.ClampMagnitude(vSteering, maxForce);

		body.velocity += vSteering;

		if (GetDistToTarget(target) < minRadiusArrival)
			Evade();

		if (isDebugOn)
		{
			debugTarget.SetTarget(target);
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void Evade()
	{
		Vector2 target = player.getPosition2D();
		Vector2 playerVel = player.getVelocity();

		Vector2 hTarget = target + playerVel * (target / maxSpeed);

		hTarget = (Vector2)this.transform.position - hTarget;
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

	void Wander()
	{
		// todo
		//StartCoroutine(WanderCoRoutine());
		float WANDER_ANGLE = 45.0f;
		float CIRCLE_RADIUS = 1.0f;
		float CIRCLE_DIST = 3.0f;
		float ANGLE_CHANGE = 0.1f;

		Vector3 circleCenter = new Vector3(
			this.body.velocity.x,
			this.body.velocity.y,
			0.0f
		);

		circleCenter = circleCenter.normalized;
		circleCenter *= CIRCLE_DIST;
		circleCenter = this.transform.position + circleCenter;

		float angle = Random.Range(-WANDER_ANGLE, WANDER_ANGLE);
		wanderDisplacement = Quaternion.Euler(0, 0, (angle * ANGLE_CHANGE) - (ANGLE_CHANGE * 0.5f)) * wanderDisplacement;
		wanderDisplacement = wanderDisplacement.normalized * CIRCLE_RADIUS;
		Vector3 target = circleCenter + wanderDisplacement;

        Vector2 hTarget = target - this.transform.position;
        Vector2 vDesired = hTarget.normalized * maxSpeed / 2;
        Vector2 vSteering = vDesired - body.velocity;
        vSteering = Vector2.ClampMagnitude(vSteering, maxForce);
        body.velocity += vSteering;
		//vSteer = vSteering;

        if (isDebugOn)
		{
			debugCircle.setCircle(circleCenter, CIRCLE_RADIUS);
			debugVelocity.SetVelocity(body.velocity);
			debugTarget.SetTarget(target);
		}
	}

	void Hide()
	{
		GameObject closestSphere = GetClosestSphere();
		if (closestSphere != null)
		{
			float distFromSphere = 3.0f;
			Vector3 heading = closestSphere.transform.position - (Vector3)player.getPosition2D();
			heading -= (Vector3)closestSphere.transform.position;
			heading = heading.normalized;
			heading *= (distFromSphere + closestSphere.GetComponent<CircleCollider2D>().radius);

			Seek(heading);
			Arrive(heading);

			if (isDebugOn)
			{
				debugTarget.SetTarget(heading);
				debugVelocity.SetVelocity(body.velocity);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
    {
		if (col.GetType() == typeof(CircleCollider2D))
			collisions.Add((CircleCollider2D)col);
		else if (col.GetType() == typeof(BoxCollider2D))
			wallCollisions.Add((BoxCollider2D)col);
    }

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.GetType() == typeof(CircleCollider2D))
			collisions.Remove((CircleCollider2D)col);
		else if (col.GetType() == typeof(BoxCollider2D))
			wallCollisions.Remove((BoxCollider2D)col);
	}

	void ObstacleAvoidance()
	{
		// TODO: AVOIDANCE
		// just because it's not it's own state, doesn't mean we shouldn't also work on avoiding obstacles during all other states

		//SphereAvoidanceMapped();
		SphereAvoidance();
		WallAvoidance();
	}

	void GroupBehavior()
	{
		// TODO: GROUP BEHAVIOR
		// rather than performing the logic here, individual bots will likely
		// get desired behavior from a controller class while in Group Behavior state

		Flee(player.getPosition2D());
		Separation();
		Alignment();
		Cohesion();	

		if (isDebugOn)
		{
			debugVelocity.SetVelocity(body.velocity);
		}
	}

	void Separation()
    {
		foreach(GameObject enemy in enemies)
        {
			Vector2 heading = this.transform.position - enemy.transform.position;
			if (heading.magnitude < 2.0f)
				Flee(enemy.transform.position);
		}
    }

	void Alignment()
    {
		//Vector2 totHeading = Vector2.zero;
		//int totEnemies = 0;
		//foreach (GameObject enemy in enemies)
		//{
		//	Vector2 heading = enemy.GetComponent<AI_Control>().GetBody().velocity;
		//	totHeading += heading;
		//	totEnemies++;
		//}
		//if (totEnemies > 0)
		//	Seek(totHeading);// / totEnemies);

		Vector2 totHeading = Vector2.zero;
		int totEnemies = 0;
		foreach (GameObject enemy in enemies)
		{
			Vector2 heading = enemy.GetComponent<AI_Control>().GetBody().velocity - (Vector2)enemy.transform.position;
			totHeading += heading;
			totEnemies++;
	    }
		if (totEnemies > 0)
			Seek(totHeading);// / totEnemies);
    }

	public Rigidbody2D GetBody()
    {
		return this.body;
    }

	void Cohesion()
    {
		foreach(GameObject enemy in enemies)

		{
			Vector2 heading = this.transform.position - enemy.transform.position;
			if (heading.magnitude > 5.0f)
				Seek(enemy.transform.position);
		}
	}

	void SphereAvoidance()
    {
		CircleCollider2D closest = null;
		float closestDist = 0.0f;
		foreach (CircleCollider2D collision in collisions)
		{
			if (collision != null && collision)
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
		}

		if (closest != null)
		{
			// Braking force
			Vector3 headingTarget = (Vector3)this.body.position - closest.transform.position;
			Vector2 brakeForce = Vector2.zero;
			float dist = headingTarget.magnitude;
			if (dist < closest.GetComponent<CircleCollider2D>().radius)
			{
				Vector3 brakeHeading = -(Vector3)this.body.velocity;
				
				brakeForce = brakeHeading / (dist / 5.0f);
				brakeForce = Vector2.ClampMagnitude(brakeForce, maxForce);
			}

			// Avoidance force
			Vector3 ahead = (Vector3)this.body.position + (Vector3)this.body.position.normalized * maxSpeed;
			Vector3 avoidanceForce = ahead - closest.transform.position;
			avoidanceForce = avoidanceForce.normalized * maxForce * 3;
			
			this.body.velocity += (Vector2)avoidanceForce + (Vector2)brakeForce;
		}

		if (isDebugOn)
		{
			debugVelocity.SetVelocity(body.velocity);
		}
	}		

	void WallAvoidance()
	{
		foreach (BoxCollider2D wall in wallCollisions)
	    {
			Vector2 wallPos = wall.transform.position;
			Vector2 aiPos = this.transform.position;
			if (wall.transform.localScale.y > wall.transform.localScale.x)
			{
				if (wallPos.x < aiPos.x) // To the left
					this.body.velocity += new Vector2(avoidanceForce, 0.0f);
				else if (wallPos.x > aiPos.x) // To the right
					this.body.velocity += new Vector2(-avoidanceForce, 0.0f);
			}
			else if (wall.transform.localScale.x > wall.transform.localScale.y)
			{
				if (wallPos.y < aiPos.y) // Below 
					this.body.velocity += new Vector2(0.0f, avoidanceForce);
				else if (wallPos.y > aiPos.y) // Above
					this.body.velocity += new Vector2(0.0f, -avoidanceForce);
			}
		}
    }

	GameObject GetClosestSphere()
	{
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
		return closest;
	}

	private float GetDistToTarget(Vector3 target)
    {
		Vector3 heading = target - this.transform.position;
		return heading.magnitude;
    }
}
