using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChoice : MonoBehaviour {

	public Text text;
	public Text friendText;
	public GameObject thinking;
	public GameObject speaking;
	public GameObject friendBubble;
	public GameObject hero;
	public GameObject friend;
	public Sprite friendWithPhone;
	private PlayerController p;
	private Sprite heroOrig;
	private Sprite friendOrig;
	private bool dialogueComplete;
	List<string> conversationLines2; // lines for the conversation between two friends
	private int convoIndex2 = 0;
	private bool first;
	public static bool fixAnim;

	private GameObject tempBubble;
	private Text tempText;

	public GameObject thoughtBubble;
	public Text thoughtText;

	public GameObject optionOne;
	public GameObject optionTwo;

	// Use this for initialization
	void Start () {
		text.text = "";
		thinking.SetActive(false);
		speaking.SetActive(false);
		p = hero.GetComponent<PlayerController> ();
		heroOrig = hero.GetComponent<SpriteRenderer> ().sprite;
		friendOrig = friend.GetComponent<SpriteRenderer> ().sprite;
		dialogueComplete = false;
		first = true;

		conversationLines2 = new List<string> ();
		conversationLines2.Add ("Are you getting a call?"); // rachel
		conversationLines2.Add ("I think so. One sec."); // friend 1
		conversationLines2.Add ("Oh my God, hey Jack!"); // friend 1
		conversationLines2.Add ("Now? Like, right now?"); // friend 1
		conversationLines2.Add ("Oh my God, I totally forgot. I'm so sorry!"); // friend 1
		conversationLines2.Add ("Ummm, I can probably be there in like fifteen minutes. Would that work?"); // friend 1
		conversationLines2.Add ("Great! I'll see you then."); // friend 1
		conversationLines2.Add ("Dude, I'm so sorry but I've got to go."); // friend 1
		conversationLines2.Add ("I totally forgot I made plans to meet up with Jack."); // friend 1
		conversationLines2.Add ("Do you remember him? From high school?"); // friend 1
		conversationLines2.Add ("I mean, yeah, high school wasn't so long ago. You have to go right now?"); // rachel
		conversationLines2.Add ("I really do, our plans started thirty minutes ago."); // friend 1
		conversationLines2.Add ("I know you're rarely in Manhattan, but you'll be okay, right?"); // friend 1
		conversationLines2.Add ("I mean, I don't live here..."); // rachel
		conversationLines2.Add ("And I have no idea how to get to Penn Station..."); // rachel
		conversationLines2.Add ("Plus my phone is running on really low battery..."); // rachel
		conversationLines2.Add ("It's just a few blocks away."); // friend 1
		conversationLines2.Add ("And I really, really have to go."); // friend 1
		conversationLines2.Add ("Tell me you'll be okay, please!"); // friend 1

		// dialogue option (rachel)

		conversationLines2.Add ("Like I said, you’ll be fine, I swear!"); // friend 1
		conversationLines2.Add ("Just look for 8th and West 31st!"); // friend 1
		conversationLines2.Add ("Call me if you need anything. Bye, girl!"); // friend 1, end cutscene

		fixAnim = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (dialogueComplete) {
			hero.GetComponent<Animator> ().enabled = true;
			friend.GetComponent<SpriteRenderer> ().flipX = true;
			friend.GetComponent<Animator> ().enabled = true;
			friend.transform.position = Vector2.MoveTowards (friend.transform.position, new Vector2(friend.transform.position.x  - 12.5f, friend.transform.position.y), 0.1f);
			if (first) {
				first = false;
				StartCoroutine (OnMyOwn ());
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (first) {
			p.CharacterPause = true;
			hero.GetComponent<Animator> ().enabled = false;
			friend.GetComponent<Animator> ().enabled = false;
			hero.GetComponent<SpriteRenderer> ().sprite = heroOrig;
			friend.GetComponent<SpriteRenderer> ().sprite = friendOrig;
			StartCoroutine ("DialogueOptions");
		}
	}

	public IEnumerator DialogueOptions () {
		
		yield return new WaitForSeconds (0.01f);
		while (convoIndex2 < conversationLines2.Count - 3) {
			if (convoIndex2 == 0) {
				tempText = text;
				tempBubble = speaking;
			} else if (convoIndex2 == 2) {
				tempText = friendText;
				tempBubble = friendBubble;
				friend.GetComponent<SpriteRenderer> ().sprite = friendWithPhone;
			} else if (convoIndex2 == 7) {
				tempText = friendText;
				tempBubble = friendBubble;
				friend.GetComponent<SpriteRenderer> ().sprite = friendOrig;
			} else if (convoIndex2 < 9) {
				tempText = friendText;
				tempBubble = friendBubble;
			} else if (convoIndex2 == 10) {
				tempText = text;
				tempBubble = speaking;
			} else if (convoIndex2 <= 12) {
				tempText = friendText;
				tempBubble = friendBubble;
			} else if (convoIndex2 <= 15) {
				tempText = text;
				tempBubble = speaking;
			} else if (convoIndex2 <= 18) {
				tempText = friendText;
				tempBubble = friendBubble;
			}

			tempText.text = conversationLines2 [convoIndex2];
			tempText.enabled = true;
			tempBubble.SetActive (true);
			yield return new WaitForSeconds (2.0f);
			tempText.enabled = false;
			tempBubble.SetActive (false);

			convoIndex2 += 1;
		}

		thinking.SetActive(true);
		optionOne.SetActive(true);
		optionTwo.SetActive (true);
		optionOne.GetComponent<Text> ().text = "Err";
		optionTwo.GetComponent<Text> ().text = "Yup";
		hero.GetComponent<Animator> ().enabled = false;
		hero.GetComponent<SpriteRenderer> ().sprite = heroOrig;

		while (text.text != "Err" && text.text != ("Yup")) {
			yield return new WaitForSeconds (.01f);
			if (Input.GetKeyDown ("1")) {
				thinking.SetActive(false);
				optionOne.SetActive(false);
				optionTwo.SetActive(false);
				text.text = "Err";
			} else if (Input.GetKeyDown ("2")) {
				thinking.SetActive(false);
				optionOne.SetActive(false);
				optionTwo.SetActive(false);
				text.text = "Yup";
			}
		}

		speaking.SetActive(true);
		text.enabled = true;
		yield return new WaitForSeconds (2.0f);
		speaking.SetActive(false);
		text.enabled = false;

		while (convoIndex2 < conversationLines2.Count) {
			friendText.text = conversationLines2 [convoIndex2];
			friendText.enabled = true;
			friendBubble.SetActive (true);
			yield return new WaitForSeconds (2.0f);
			friendText.enabled = false;
			friendBubble.SetActive (false);
			convoIndex2 += 1;
		}

		fixAnim = true;
		p.CharacterPause = false;
		dialogueComplete = true;
	}

	public IEnumerator OnMyOwn() {
		yield return new WaitForSeconds (0.8f);

		thoughtBubble.SetActive (true);
		thoughtText.enabled = true;
		thoughtText.text = "I guess I'm on my own then...";
		yield return new WaitForSeconds (2.0f);
		thoughtBubble.SetActive (false);
		thoughtText.enabled = false;
	}
}

