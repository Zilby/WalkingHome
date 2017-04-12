using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillVisibilityController : MonoBehaviour {

	bool visibileFill; // determines if the fill will be visible or not.

	// Use this for initialization
	void Start () {
		visibileFill = false; // initializes the fill determinant to be not filled
		ToggleVisibility (); // toggles the visibility of the fill based on the bool field above
	}
	
	// Update is called once per frame
	void Update () {
		ToggleVisibility (); // toggles the visibility of the fill based on the bool field above
	}

	// toggles the visibility of the fill based on the bool field above
	void ToggleVisibility() {
		if (visibileFill) {
			// make it visible
		} else {
			// don't
		}
	}
}
