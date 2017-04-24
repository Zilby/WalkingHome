using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this one goes out to all the boys and girls out there who don't know how naming conventions work for game programming
// -- much love, another programming-challenged coder
public class StatController : MonoBehaviour {

	public GameObject textObj;
	private Color textColor;

	// Use this for initialization
	void Start () {
		textColor = new Color (0f, 0f, 0f, .5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (statChangeEvent) { // how is event handling processed
			// show the object 
			// set the text
			// set the text color
			textObj.transform.position = Vector2.up * new Vector2 (.25f, .25f);
			// fade the text
		}
		if (textObj.transform.position == new Vector2(0,0) || textColor.a <= 0) { // TODO determine actual position 
			// reset the position
			// hide the object
		}
	}

	void pickColor(int statType) {
		if (statType == 0) {
			// set color to confidence
		} else if (statType == 1) {
			// set color to paranoia
		} else {
			// set color to frustration
		}
	}

	string setText(int value) {
		return value.ToString ();
	}
}
