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
	public GameObject stat; // to be created upon stat changes
	public static List<string> responses = new List<string>(5);

	private Sprite heroOrig; // represents the player's starting sprite
	private bool optionSelected;
	private bool first;
	private Vector3 heroPos;

	// Use this for initialization
	void Start () {
		heroOrig = hero.GetComponent<SpriteRenderer> ().sprite;
		optionSelected = false;
		first = true;

		responses.Add("Could you leave me alone? Thanks.");
		responses.Add("Hey, maybe you should try respecting women sometime.");
		responses.Add("You know I'm a human being, right?");
		responses.Add("You know, I'd atually rather not be catcalled right now.");
		responses.Add("Concept: Maybe don't harass women on the street?");
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

	/* void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.name.Equals("Hero") && first) {
			StartCoroutine(PauseGame ()); // give the player some time to make a decision -> freeze everything else!
			StartCoroutine(DialogueOptions ()); // the player has an opportunity to impact the game
			first = false;
		}
	} */

	public IEnumerator DialogueOptions () {
		bool choiceSelected = false;
		yield return new WaitForSeconds (0.01f); // for stableness of enum, match other simultaneous enum? TODO

		thinking.SetActive (true); // display the thought bubble
		optionOne.SetActive (true); // display the 1 key
		optionTwo.SetActive (true); // display the 2 key
		optionOneText.SetActive (true); // display the first option 
		optionTwoText.SetActive (true); // display the second option
		optionOneText.GetComponent<Text> ().text = "I'll keep walking..."; // retitle options
		optionTwoText.GetComponent<Text> ().text = "*Retaliate*"; // retitle options
		hero.GetComponent<Animator> ().enabled = false; // stop hero's walking
		hero.GetComponent<SpriteRenderer> ().sprite = heroOrig; // reset hero's sprite

		GameObject f = Instantiate (stat);
		GameObject c = Instantiate (stat);
		GameObject p = Instantiate (stat);

		heroPos = hero.transform.position;

		f.transform.position = new Vector3 (heroPos.x, heroPos.y + 10, heroPos.z + 0.5f);
		c.transform.position = new Vector3 (heroPos.x + 0.5f, heroPos.y + 10, heroPos.z + 0.5f);
		p.transform.position = new Vector3 (heroPos.x - 0.5f, heroPos.y + 10, heroPos.z + 0.5f);

		StatController fs = f.GetComponent<StatController> ();
		StatController cs = c.GetComponent<StatController> ();
		StatController ps = p.GetComponent<StatController> ();

		fs.pickColor (2); 
		cs.pickColor (0);
		ps.pickColor (1);

		// waiting to pick an option, or until time runs out
		while (!optionSelected) {
			yield return new WaitForSecondsRealtime (.01f);
			if (Input.GetKeyDown ("1")) {
				speech.GetComponent<Text>().text = "*" + optionOneText.GetComponent<Text> ().text + "*"; 
				optionSelected = true;
				StopCoroutine (PauseGame ());
				Time.timeScale = 1.0f;
				GameController.confidence -= 5;
				GameController.paranoia += 10;
				cs.change = "-5";
				ps.change = "+10";
			} else if (Input.GetKeyDown ("2")) {
				int randomIndex = Random.Range (0, responses.Count);
				speech.GetComponent<Text>().text = responses[randomIndex]; 
				optionSelected = true;
				choiceSelected = true;
				StopCoroutine (PauseGame ());
				Time.timeScale = 1.0f;
				if (GameController.confidence > 35) {
					// enemy makes no response
					GameController.confidence += 5;
					cs.change = "+5";
				} else {
					// enemy says something
					GameController.confidence -= 10;
					GameController.paranoia += 20;
					cs.change = "-10";
					ps.change = "+20";
				}
			}
		}

		GameController.frustration += 5;
		fs.change = "+5";

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

		int pauseTime = 0;
		while (!optionSelected && pauseTime < 50) {
			yield return new WaitForSecondsRealtime (0.1f);
			pauseTime++;
		}

		optionSelected = true;
		Time.timeScale = 1.0f; 
	}
}
