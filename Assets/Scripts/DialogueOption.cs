using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOption : MonoBehaviour {

	public GameObject thinking; // represents the thought bubble
	public GameObject optionOne; // represents the 1 key image
	public GameObject optionTwo; // represents the 2 key image
	public GameObject optionOneText; // represents the associated choice
	public GameObject optionTwoText; // represents the associated choice

	public GameObject speaking; // represents the speech bubble
	public GameObject speech; // represents the words in the speech bubble

	public GameObject hero; // represents the player
	public GameObject stat; // to be created upon stat changes, using a prefab

	public static List<string> responses = new List<string>(5); // set list of retorts

	private Sprite heroOrig; // represents the player's starting sprite
	private bool first; // to ensure the encounter only occurs once
	private Vector3 heroPos; // hero's position
	private int pauseTime = 0;
	private bool stopPause = false;

	GameObject temp; // dumb temporary variable to fix some code

	// Use this for initialization
	void Start () {
		heroOrig = hero.GetComponent<SpriteRenderer> ().sprite;
		first = true;

		responses.Add("Could you leave me alone? Thanks.");
		responses.Add("Hey, maybe you should try respecting women sometime.");
		responses.Add("You know I'm a human being, right?");
		responses.Add("You know, I'd actually rather not be catcalled right now.");
		responses.Add("Concept: Maybe don't harass women on the street?");
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.name.Equals("Herobox") && first) {
			pauseTime = 0;
			stopPause = false;
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

		yield return new WaitForSeconds (0.01f); 

		bool choiceSelected = false; // representing the fact that 1/2 wasnt pressed

		// set the thoughts up
		thinking.SetActive (true); // display the thought bubble
		optionOne.SetActive (true); // display the 1 key
		optionTwo.SetActive (true); // display the 2 key
		optionOneText.SetActive (true); // display the first option 
		optionTwoText.SetActive (true); // display the second option
		optionOneText.GetComponent<Text> ().text = "I'll keep walking..."; // retitle options
		optionTwoText.GetComponent<Text> ().text = "*Retaliate*"; // retitle options

		// stop the hero's movement visually
		hero.GetComponent<Animator> ().enabled = false; // stop hero's walking
		hero.GetComponent<SpriteRenderer> ().sprite = heroOrig; // reset hero's sprite

		// waiting to pick an option, or until time runs out
		while (!choiceSelected) {

			yield return new WaitForSecondsRealtime (.01f);

			if (Input.GetKeyDown ("1")) {
				GameObject f = Instantiate (stat);
				temp = f;

				GameObject c = Instantiate (stat);
				GameObject p = Instantiate (stat);

				heroPos = hero.transform.position;

				f.transform.position = new Vector3 (heroPos.x, heroPos.y + 1.5f, heroPos.z - 5f);

				c.transform.position = new Vector3 (heroPos.x + 1.5f, heroPos.y + 1.5f, heroPos.z - 5f);
				p.transform.position = new Vector3 (heroPos.x - 1.5f, heroPos.y + 1.5f, heroPos.z - 5f);

				StatController cs = c.GetComponent<StatController> ();
				StatController ps = p.GetComponent<StatController> ();
				StatController fs = temp.GetComponent<StatController> ();

				cs.pickColor (0);
				ps.pickColor (1);
				fs.pickColor (2);

				GameController.frustration += 5;
				GameController.confidence -= 5;
				GameController.paranoia += 10;

				fs.change = "+5";
				cs.change = "-5";
				ps.change = "+10";
				 
				choiceSelected = true; // TODO
				StopCoroutine (PauseGame ()); // TODO
				stopPause = true;
				Time.timeScale = 1.0f; // TODO redundant?
			} else if (Input.GetKeyDown ("2")) {
				
				GameObject f = Instantiate (stat);
				temp = f;
				GameObject c = Instantiate (stat);
				GameObject p = Instantiate (stat);

				heroPos = hero.transform.position;

				f.transform.position = new Vector3 (heroPos.x, heroPos.y + 1.5f, heroPos.z - 5f);
				c.transform.position = new Vector3 (heroPos.x + 1.5f, heroPos.y + 1.5f, heroPos.z - 5f);
				p.transform.position = new Vector3 (heroPos.x - 1.5f, heroPos.y + 1.5f, heroPos.z - 5f);

				StatController cs = c.GetComponent<StatController> ();
				StatController ps = p.GetComponent<StatController> ();
				StatController fs = temp.GetComponent<StatController> ();

				GameController.frustration += 5;
				fs.change = "+5";
				 
				cs.pickColor (0);
				ps.pickColor (1);
				fs.pickColor (2);

				int randomIndex = Random.Range (0, responses.Count); // pick a random index
				speech.GetComponent<Text>().text = responses[randomIndex]; // get a random response

				choiceSelected = true;

				StopCoroutine (PauseGame ());
				stopPause = true;
				Time.timeScale = 1.0f;

				Debug.Log ("IFELSEVAL: " + ((int)GameController.confidence + Random.Range (0, 30)).ToString());
				if (GameController.confidence + Random.Range(0, 30) > 85) {
					StopCoroutine(GetComponent<EnemyController>().Catcall ());

					GameController.confidence += 5;
					cs.change = "+5";
					ps.change = "+0";
				} else {
					StopCoroutine (GetComponent<EnemyController>().Catcall ());
					StartCoroutine (GetComponent<EnemyController>().Catcall2 ());

					GameController.confidence -= 10;
					GameController.paranoia += 20;
					cs.change = "-10";
					ps.change = "+20";
				}
			}
		}

		thinking.SetActive(false);
		optionOne.SetActive(false);
		optionTwo.SetActive(false);
		optionOneText.SetActive(false);
		optionTwoText.SetActive(false);

		/*
		if (optionSelected) {
			GameObject f = Instantiate (stat);
			temp = f;
			GameObject c = Instantiate (stat);
			GameObject p = Instantiate (stat);

			heroPos = hero.transform.position;

			f.transform.position = new Vector3 (heroPos.x, heroPos.y + 1.5f, heroPos.z - 5f);
			c.transform.position = new Vector3 (heroPos.x + 1.5f, heroPos.y + 1.5f, heroPos.z - 5f);
			p.transform.position = new Vector3 (heroPos.x - 1.5f, heroPos.y + 1.5f, heroPos.z - 5f);

			StatController cs = c.GetComponent<StatController> ();
			StatController ps = p.GetComponent<StatController> ();
			StatController fs = f.GetComponent<StatController> ();

			cs.pickColor (0);
			ps.pickColor (1);
			fs.pickColor (2);
		}

		optionSelected = false;

		if (choiceSelected) {
			speaking.SetActive(true); // speech bubble is enabled
			speech.SetActive(true); // words in bubble are enabled
			yield return new WaitForSecondsRealtime (2.0f); // speech stays active for a while
			speaking.SetActive(false); // speech bubble is hidden
			speech.SetActive(false); // words in bubble are disabled
		}
		choiceSelected = false; */
	}
		
	public IEnumerator PauseGame() {

		yield return new WaitForSeconds (0.01f); // for stableness of enum
		Time.timeScale = 0.0f; // stops the game

		while (pauseTime < 50 && !stopPause) {
			Debug.Log (pauseTime);
			yield return new WaitForSecondsRealtime (0.1f);
			pauseTime++;
		}

		if (!stopPause) {
			GameObject f = Instantiate (stat);
			temp = f;

			GameObject c = Instantiate (stat);
			GameObject p = Instantiate (stat);

			heroPos = hero.transform.position;

			f.transform.position = new Vector3 (heroPos.x, heroPos.y + 1.5f, heroPos.z - 5f);

			c.transform.position = new Vector3 (heroPos.x + 1.5f, heroPos.y + 1.5f, heroPos.z - 5f);
			p.transform.position = new Vector3 (heroPos.x - 1.5f, heroPos.y + 1.5f, heroPos.z - 5f);

			StatController cs = c.GetComponent<StatController> ();
			StatController ps = p.GetComponent<StatController> ();
			StatController fs = temp.GetComponent<StatController> ();

			cs.pickColor (0);
			ps.pickColor (1);
			fs.pickColor (2);

			GameController.frustration += 5;
			GameController.confidence -= 5;
			GameController.paranoia += 10;

			fs.change = "+5";
			cs.change = "-5";
			ps.change = "+10";
		}

		thinking.SetActive(false);
		optionOne.SetActive(false);
		optionTwo.SetActive(false);
		optionOneText.SetActive(false);
		optionTwoText.SetActive(false);

		Time.timeScale = 1.0f;
	}
}
