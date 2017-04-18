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

	private GameObject tempBubble;
	private Text tempText;

	public GameObject thoughtBubble;
	public Text thoughtText;

	// Use this for initialization
	void Start () {
		text.text = "";
		thinking.SetActive(false);
		speaking.SetActive(false);
		p = hero.GetComponent<PlayerController> ();
		heroOrig = hero.GetComponent<SpriteRenderer> ().sprite;
		friendOrig = friend.GetComponent<SpriteRenderer> ().sprite;
		dialogueComplete = false;

		conversationLines2 = new List<string> ();
		conversationLines2.Add ("Are you getting a call?"); // rachel
		conversationLines2.Add ("I think so. One sec."); // friend 1
		conversationLines2.Add ("Oh my God, hey Jack!"); // friend 1
		conversationLines2.Add ("Now? Like, right now?"); // friend 1
		conversationLines2.Add ("Oh my God, I totally forgot. I'm so sorry!"); // friend 1
		conversationLines2.Add ("Ummm, I can probably be there in like fifteen minutes. Would that work?"); // friend 1
		conversationLines2.Add ("Great! I'll see you then."); // friend 1
		conversationLines2.Add ("Dude, I'm so sorry but I've got to go. I totally forgot I made plans to meet up with Jack. Do you remember him? From high school?"); // friend 1
		conversationLines2.Add ("I mean, yeah, high school wasn't so long ago. You have to go right now?"); // rachel
		conversationLines2.Add ("I really do, our plans started thirty minutes ago. I know you're rarely in Manhattan, but you'll be okay, right?"); // friend 1
		conversationLines2.Add ("I mean, I don't live here... And I have no idea how to get to Penn Station... Plus my phone is running on really low battery..."); // rachel
		conversationLines2.Add ("It's just a few blocks away. And I really, really have to go. Tell me you'll be okay, please!"); // friend 1

		// dialogue option (rachel)

		conversationLines2.Add ("Like I said, you’ll be fine, I swear! Just look for 8th and West 31st! Call me if you need anything. Bye, girl!"); // friend 1, end cutscene
	}
	
	// Update is called once per frame
	void Update () {
		if (dialogueComplete) {
			hero.GetComponent<Animator> ().enabled = true;
			friend.GetComponent<SpriteRenderer> ().flipX = true;
			friend.GetComponent<Animator> ().enabled = true;
			friend.transform.position = Vector2.MoveTowards (friend.transform.position, new Vector2(friend.transform.position.x  - 12.5f, friend.transform.position.y), 0.1f);
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		p.CharacterPause = true;
		hero.GetComponent<Animator> ().enabled = false;
		hero.GetComponent<SpriteRenderer> ().sprite = heroOrig;
		StartCoroutine ("DialogueOptions");
	}

	public IEnumerator DialogueOptions () {
		
		yield return new WaitForSeconds (0.01f);
		while (convoIndex2 < conversationLines2.Count - 1) {
			if (convoIndex2 == 0) {
				tempText = text;
				tempBubble = speaking;
			} else if (convoIndex2 == 2) {
				tempText = friendText;
				tempBubble = friendBubble;
				friend.GetComponent<SpriteRenderer> ().sprite = friendWithPhone;
			} else if (convoIndex2 < 7) {
				tempText = friendText;
				tempBubble = friendBubble;
			} else if (convoIndex2 == 7) {
				tempText = friendText;
				tempBubble = friendBubble;
				friend.GetComponent<SpriteRenderer> ().sprite = friendOrig;
			} else if (convoIndex2 == 8) {
				tempText = text;
				tempBubble = speaking;
			} else if (convoIndex2 == 9) {
				tempText = friendText;
				tempBubble = friendBubble;
			} else if (convoIndex2 == 10) {
				tempText = text;
				tempBubble = speaking;
			} else if (convoIndex2 == 11) {
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
		hero.GetComponent<Animator> ().enabled = false;
		hero.GetComponent<SpriteRenderer> ().sprite = heroOrig;

		while (text.text != "Option one" && text.text != ("Option two")) {
			yield return new WaitForSeconds (.01f);
			if (Input.GetKeyDown ("1")) {
				thinking.SetActive(false);
				text.text = "Option one";
			} else if (Input.GetKeyDown ("2")) {
				thinking.SetActive(false);
				text.text = "Option two";
			}
		}

		speaking.SetActive(true);
		text.enabled = true;
		yield return new WaitForSeconds (2.0f);
		speaking.SetActive(false);
		text.enabled = false;

		friendText.text = conversationLines2 [conversationLines2.Count - 1];
		friendText.enabled = true;
		friendBubble.SetActive (true);
		yield return new WaitForSeconds (2.0f);
		friendText.enabled = false;
		friendBubble.SetActive (false);

		thoughtBubble.SetActive (true);
		thoughtText.enabled = true;
		thoughtText.text = "I guess I'm on my own then...";
		yield return new WaitForSeconds (2.0f);
		thoughtBubble.SetActive (false);
		thoughtText.enabled = false;

		p.CharacterPause = false;
		dialogueComplete = true;
	}
}

