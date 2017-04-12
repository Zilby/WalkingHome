using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtsController : MonoBehaviour {

	public Text text; // the thought to be displayed

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>(); // gets the thought to be displayed
	}
	
	// Update is called once per frame
	void Update () {
		// this if-elseif-else block determines the thoughts displayed based on time passed since the level loaded
		if (Time.timeSinceLevelLoad >= 5.0f && Time.timeSinceLevelLoad <= 10.0f) {
			text.text = "These people make me really nervous";
		} else if (Time.timeSinceLevelLoad >= 5.0f && Time.timeSinceLevelLoad <= 15.0f) {
			text.text = "I just wish they would leave me alone";
		} else {
			text.text = "These people are the absolute worst.";
		}
	}
}