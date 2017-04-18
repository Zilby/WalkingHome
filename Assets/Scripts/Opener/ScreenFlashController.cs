using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFlashController : MonoBehaviour {

	public Image screenFilter;

	private float alphaValue;
	private int counter;
	private Color newColor;
	public string scene;


	// Use this for initialization
	void Start () {
		screenFilter = GetComponent<Image> ();
		counter = 0;
		alphaValue = 1.0f;
	}

	// Update is called once per frame
	void Update () {
		if (alphaValue >= 0.0f) {
			if (counter >= 5) {
				alphaValue -= 0.03f;
				counter = 0;
			}
		}
		counter += 1;
		newColor = new Color (screenFilter.color.r, screenFilter.color.g, screenFilter.color.b, alphaValue);
		screenFilter.color = newColor;

		if (counter >= 85) {
			GameController.frustration -= 25;
			if (GameController.frustration < 0) {
				GameController.frustration = 0;
			}
			SceneManager.LoadScene (scene);
		}
	}
}

/*
 * if (counter == 15) {
			screenFilter.enabled = !screenFilter.enabled;
			counter = 0;
		}
		counter += 1;
  */
