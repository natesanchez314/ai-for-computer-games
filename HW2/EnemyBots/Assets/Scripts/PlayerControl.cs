using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An script to control the Player with mouse input 

public class PlayerControl : MonoBehaviour 
{
	public enum PlayerState
	{
		NORMAL,
		MOVING
	}

	public float speed;

	private Rigidbody2D body;
	private PlayerState state;
	private Vector3 movetarget;

	void Start () {

		body = GetComponent<Rigidbody2D> ();
		state = PlayerState.NORMAL;
	}

	// FixedUpdate is like update, but it occurs in step with the physics calculations
	// We're using this since we're making asjustments to the RigidBody, which controls physics for this game object
	void FixedUpdate () 
	{
		// check for fire input
		if (Input.GetButton ("Fire1")) 
		{
			movetarget = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			movetarget.z = transform.position.z;
			state = PlayerState.MOVING;
		}

		// move towards taraget
		if (state == PlayerState.MOVING) 
		{
			Vector3 dir = movetarget - transform.position;

			if (dir.magnitude < 1.0f) 
			{
				state = PlayerState.NORMAL;
				body.velocity = Vector3.zero;
			}
			else 
			{
				dir.Normalize ();
				body.velocity = dir * speed;
				float angle = Mathf.Atan2 (body.velocity.y, body.velocity.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			}
		}
	}

	public Vector2 getPosition2D()
	{
		return new Vector2 (transform.position.x, transform.position.y);
	}

	public Vector2 getVelocity()
	{
		if (body == null) 
		{
			body = GetComponent<Rigidbody2D>();
		}
		return body.velocity;
	}
}
