using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCircle : MonoBehaviour {

	LineRenderer render;
	public int segments;

	// Use this for initialization
	void Start () {
		render = GetComponentInChildren<LineRenderer> ();

		render.positionCount = segments + 1;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setCircle(Vector3 center, float radius)
	{
		float x = 0;
		float y = 0;
		float angle = 0;

		for (int i = 0; i < segments + 1; i++) {
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
			y = Mathf.Cos (Mathf.Deg2Rad * angle) * radius;

			render.SetPosition (i, new Vector3 (x, y,20) + center);

			angle += (360.0f / segments);
		}

	}

	public void Clear()
	{
		for (int i = 0; i < segments + 1; i++) {
			render.SetPosition (i, Vector3.zero);

		}
	}

}
