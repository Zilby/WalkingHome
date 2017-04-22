using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceText : MonoBehaviour {

	public List<string> endWords;
	public Text spacePrompt;
	int wordIndex;

	// Use this for initialization
	void Start () {
		wordIndex = 1;
		GetComponent<Text> ().text = endWords [0];
		spacePrompt.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (wordIndex == 2) {
			spacePrompt.enabled = false;
		}

		if (wordIndex >= endWords.Count + 1) {
			if (SceneManager.GetActiveScene().name.Equals("OpenerText")) {
				SceneManager.LoadScene ("Scroller");
			} else if (SceneManager.GetActiveScene().name.Equals("CreditsTransition")) {
				SceneManager.LoadScene ("Credits");
			}
		}

		if (Input.GetKeyDown("space") && wordIndex < endWords.Count) {
			GetComponent<Text> ().text = endWords [wordIndex];
			wordIndex += 1;
		} else if (Input.GetKeyDown ("space")) {
			wordIndex += 1;
		}
	}
}
