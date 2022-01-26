using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTarget : MonoBehaviour {

	LineRenderer render;

	// Use this for initialization
	void Start () {
		render = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetTarget(Vector2 target)
	{
		render.SetPosition (0, new Vector3(transform.position.x, transform.position.y, 20));
		render.SetPosition (1, new Vector3(target.x, target.y, 20));
	}

	public void Clear()
	{
		render.SetPosition (0, Vector3.zero);
		render.SetPosition (1, Vector3.zero);
	}
}
