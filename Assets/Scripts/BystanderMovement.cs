﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.x >= -50.0f) {
			transform.position = new Vector3 (transform.position.x - 0.05f, transform.position.y, transform.position.z);
		} else {
			Destroy (gameObject);
		}
	}
}
