using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script used to destroy a GameObject that has a trigger Collider2D component
public class VisibilityController : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col) {
		Destroy (gameObject);
	}
}
