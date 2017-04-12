using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeController : MonoBehaviour {

	public Text text;
	public float fadeFloat;

	private int counter;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		fadeFloat = 0.0f;
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeFloat <= 255.0f) {
			if (counter >= 30) {
				fadeFloat += 1.0f;
				counter = 0;
			}
		}

		text.color = new Color (fadeFloat, fadeFloat, fadeFloat);
		counter += 1;
	}
}
