using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script is intended to be mainly affected by trigger events of enemies - when they collide with the player
// the player will have 5 seconds to make a choice, or it will default to saying nothing and continuing to walk
// this specification is not final, nor is it particularly effective as it stands
public class DialogueOption : MonoBehaviour {

	public GameObject thinking; // represents the thought bubble
	public GameObject speaking; // represents the speech bubble
	public GameObject speech; // represents the words in the speech bubble
	public GameObject optionOne; // represents the 1 key image
	public GameObject optionTwo; // represents the 2 key image
	public GameObject optionOneText; // represents the associated choice
	public GameObject optionTwoText; // represents the associated choice
	public GameObject hero; // represents the player
	private Sprite heroOrig; // represents the player's starting sprite
	private bool optionSelected;
	private bool first;

	// Use this for initialization
	void Start () {
		heroOrig = hero.GetComponent<SpriteRenderer> ().sprite;
		optionSelected = false;
		first = true;
	}
	
	// Update is called once per frame
	void Update () {
		// do anything that needs to happen essentially on tick
		// TODO determine if we need anything in this function, if not => remove it
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.name.Equals("Herobox") && first) {
			StartCoroutine(PauseGame ()); // give the player some time to make a decision -> freeze everything else!
			StartCoroutine(DialogueOptions ()); // the player has an opportunity to impact the game
			first = false;
		}
	}

	public IEnumerator DialogueOptions () {
		bool choiceSelected = false;
		yield return new WaitForSeconds (0.01f); // for stableness of enum, match other simultaneous enum? TODO

		thinking.SetActive (true); // display the thought bubble
		optionOne.SetActive (true); // display the 1 key
		optionTwo.SetActive (true); // display the 2 key
		optionOneText.SetActive (true); // display the first option 
		optionTwoText.SetActive (true); // display the second option
		optionOneText.GetComponent<Text> ().text = "I'll keep walking"; // retitle options
		optionTwoText.GetComponent<Text> ().text = "I won't let this slide"; // retitle options
		hero.GetComponent<Animator> ().enabled = false; // stop hero's walking
		hero.GetComponent<SpriteRenderer> ().sprite = heroOrig; // reset hero's sprite

		// waiting to pick an option, or until time runs out
		while (!optionSelected) {
			yield return new WaitForSecondsRealtime (.01f);
			if (Input.GetKeyDown ("1")) {
				speech.GetComponent<Text>().text = "*" + optionOneText.GetComponent<Text> ().text + "*"; 
				optionSelected = true;
				StopCoroutine (PauseGame ());
				Time.timeScale = 1.0f;
			} else if (Input.GetKeyDown ("2")) {
				speech.GetComponent<Text>().text = optionTwoText.GetComponent<Text> ().text; 
				optionSelected = true;
				choiceSelected = true;
			}
		}
			
		thinking.SetActive(false);
		optionOne.SetActive(false);
		optionTwo.SetActive(false);
		optionOneText.SetActive(false);
		optionTwoText.SetActive(false);
		optionSelected = false;

		if (choiceSelected) {
			speaking.SetActive(true); // speech bubble is enabled
			speech.SetActive(true); // words in bubble are enabled
			yield return new WaitForSecondsRealtime (2.0f); // speech stays active for a while
			speaking.SetActive(false); // speech bubble is hidden
			speech.SetActive(false); // words in bubble are disabled
		}
		choiceSelected = false;
	}
		
	public IEnumerator PauseGame() {

		yield return new WaitForSeconds (0.01f); // for stableness of enum
		Time.timeScale = 0.0f; // stops the game

		int counter = 0;
		while (!optionSelected && counter < 50) {
			yield return new WaitForSecondsRealtime (0.1f);
			counter++;
		}

		optionSelected = true;
		Time.timeScale = 1.0f; 
	}
}
