using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFlashController : MonoBehaviour {

	public Image screenFilter;
	public GameObject filter;

	private float alphaValue;
	private int counter;
	private Color newColor;

	// Use this for initialization
	void Start () {
		screenFilter = GetComponent<Image> ();
		filter = screenFilter.gameObject;
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

		// added a change here so that the transition can be used elsewhere by naming the image gameobject something unique
		if (counter >= 85 && filter.name.Equals ("OpenerTextFilter")) {
			SceneManager.LoadScene ("Scroller");
		} else if (counter >= 85 && filter.name.Equals("TransitionFilter")) {
			SceneManager.LoadScene ("PixelCityMaze_Huh");
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
