using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour {

	private Image faderImage;
	private int counter;
	private float alpha;

	void Start() {
		faderImage = GetComponent<Image> ();
		counter = 0;
		alpha = 0.0f;
	}

	void Update() {
		
		if (alpha < 1.0f) {
			if (counter == 30) {
				alpha += .1f;
				counter = 0;
			}
		}

		if (counter >= 45) {
			SceneManager.LoadScene ("TitleScreen");
		}

		counter += 1;
		faderImage.color = new Color (faderImage.color.r, faderImage.color.r, faderImage.color.r, alpha);
	}
}
