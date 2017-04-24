using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this one goes out to all the boys and girls out there who don't know how naming conventions work for game programming
// -- much love, another programming-challenged coder
public class StatController : MonoBehaviour {

	public GameObject textObj;
	public int speed = 1;

	private Color textColor;
	private float alphaVal;
	private Vector2 initPos;

	// Use this for initialization
	void Start () {
		textColor = new Color (0f, 0f, 0f, .5f);
	}

	void FixedUpdate () {
		if (statChangeEvent) { // how is event handling processed
			textObj.SetActive(true); 
			textObj.GetComponent<Text> ().text = this.setText (1);
			// set the text color
			textObj.transform.position = Vector2.up * new Vector2 (.01f, .01f);
			// fade the text
		}
		if (textObj.transform.position == new Vector2(initPos.x, initPos.y + .25f) || textColor.a <= 0) { // TODO determine actual position 
			textObj.transform.position = initPos;
			textObj.SetActive (false);
		}
	}

	void pickColor(int statType) {
		if (statType == 0) {
			// set color to confidence
			textColor = new Color(86f, 185f, 255f, alphaVal);
		} else if (statType == 1) {
			// set color to paranoia
			textColor = new Color(246f, 249f, 0f, alphaVal);
		} else {
			// set color to frustration
			textColor = new Color(255f, 0f, 0f, alphaVal);
		}
	}

	string setText(int value) {
		return value.ToString ();
	}
}
