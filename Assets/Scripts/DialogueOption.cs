using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOption : MonoBehaviour {

	public GameObject thinking;
	public GameObject speaking;
	public GameObject optionOne;
	public GameObject optionTwo;
	public GameObject hero;
	private Sprite heroOrig;

	// Use this for initialization
	void Start () {
		// initalize all the gameobjects and stuff from above
		heroOrig = hero.GetComponent<SpriteRenderer> ().sprite;


	}
	
	// Update is called once per frame
	void Update () {
		// do anything that needs to happen essentially on tick
	}

	void OnTriggerEnter2D (Collider2D col) {
		StartCoroutine(PauseGame ()); // give the player some time to make a decision -> freeze everything else!
		StartCoroutine(DialogueOptions ()); // the player has an opportunity to impact the game
	}

	public IEnumerator DialogueOptions () {

		yield return new WaitForSeconds (0.01f);

		thinking.SetActive(true); // display the thought bubble
		optionOne.SetActive(true); // display the 1 key
		optionTwo.SetActive (true); // display the 2 key
		optionOne.GetComponent<Text> ().text = "I'll keep walking"; // retitle options
		optionTwo.GetComponent<Text> ().text = "I won't let this slide"; // retitle options
		hero.GetComponent<Animator> ().enabled = false; // stop hero's walking
		hero.GetComponent<SpriteRenderer> ().sprite = heroOrig; // reset hero's sprite

		// waiting to pick an option... need to include time running out TODO
		while (text.text != "Err" && text.text != ("Yup")) {
			yield return new WaitForSeconds (.01f);
			if (Input.GetKeyDown ("1")) {
				thinking.SetActive(false);
				optionOne.SetActive(false);
				optionTwo.SetActive(false);
				speaking.GetComponent<Text> ().text = optionOne.GetComponent<Text> ().text;
			} else if (Input.GetKeyDown ("2")) {
				thinking.SetActive(false);
				optionOne.SetActive(false);
				optionTwo.SetActive(false);
				speaking.GetComponent<Text> ().text = optionTwo.GetComponent<Text> ().text;
			}
		}

		speaking.SetActive(true); // speech bubble is enabled
		speaking.GetComponent<Text>().text. = true; // text is shown on bubble
		yield return new WaitForSeconds (2.0f); // speech stays active for a while
		speaking.SetActive(false); // speech bubble is hidden

		// the retaliation is done, so we can resume the game and such TODO
	}

	// TODO rename
	public IEnumerator PauseGame() {

		yield return new WaitForSeconds (0.01f); // for stableness of enum
		Time.timeScale = 0.0f; // stops the game

		yield return new WaitForSecondsRealtime (5.0f); // give the player five seconds of time to make a choice
		Time.timeScale = 1.0f; // TODO make sure 1.0 is the default
	}
}
