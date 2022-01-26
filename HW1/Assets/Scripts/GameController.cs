using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject peonPrefab;

    Castle castleRef;
    Mine mineRef;
    Forest forestRef;

    public float gold;

	// Use this for initialization
	void Start () {
        castleRef = GameObject.FindGameObjectWithTag("Castle").GetComponent<Castle>();
        mineRef = GameObject.FindGameObjectWithTag("Mine").GetComponent<Mine>();
        forestRef = GameObject.FindGameObjectWithTag("Forest").GetComponent<Forest>();
    }
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space))
		{
            if (gold >= 100)
            {
                Instantiate(peonPrefab, castleRef.transform.position, Quaternion.identity);
                gold -= 100;
            }
		}

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null)
            {
                Peon p = hit.collider.gameObject.GetComponent<Peon>();

                p.OnClick();
            }

        }
	}
}
