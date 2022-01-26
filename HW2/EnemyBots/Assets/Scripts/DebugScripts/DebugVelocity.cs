using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVelocity : MonoBehaviour {

	LineRenderer render;

	// Use this for initialization
	void Start () {
		render = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetVelocity(Vector2 velocity)
	{
		Vector3 temp = new Vector3 (transform.position.x, transform.position.y, 20);
		render.SetPosition (0, temp);
		render.SetPosition (1, temp + new Vector3(velocity.x, velocity.y, 0));
	}

	public void Clear()
	{
		render.SetPosition (0, Vector3.zero);
		render.SetPosition (1, Vector3.zero);
	}
}
