using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointAIController : MonoBehaviour {

	GameObject enemy;
	float xPos;
	float yPos;

	// Use this for initialization
	void Start () {
		enemy = GameObject.FindGameObjectWithTag ("enemy");
		xPos = enemy.transform.position.x;
		yPos = enemy.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		// + 3.3 in the y direction
	}
}
