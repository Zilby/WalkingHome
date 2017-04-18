using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.x >= 0) {
			gameObject.transform.position = new Vector2 (gameObject.transform.position.x - 1.0f, gameObject.transform.position.y);
		} else {
			Destroy (gameObject);
		}
	}
}
