using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Friend : MonoBehaviour {

	public GameObject hero; // will be set in the inspector
	public GameObject otherFriend; // will be set in the inspector

	public GameObject thinking; // thought bubble for hero

	public GameObject heroBubble; // speech bubble for hero
	public Text heroText; // text in the bubble

	public GameObject friendBubble; // speech bubble for friend1
	public Text friendText; // text in the bubble

	public GameObject friend2Bubble; // speech bubble for friend 2
	public Text friend2Text; // text in the bubble

	public GameObject tempBubble;
	public Text tempText;

	public Text moveText;
	public bool independent;

	private PlayerController p; // to pause the player's movement
	// public Animator anim; // animator for the hero?

	private Sprite heroOrig;
	private Sprite friendOrig;
	private Sprite otherFriendOrig;

	List<string> conversationLines1; // lines for the first conversation between three friends

	private int convoIndex1;
	private bool cutBool;
	private bool stopWalking;
	private bool friend2Leave;
	private bool playerNotMoving = true;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1.0f;
		p = hero.GetComponent<PlayerController> ();
		p.CharacterPause = true;
		// anim.enabled = false;

		heroOrig = hero.GetComponent<SpriteRenderer>().sprite;
		friendOrig = gameObject.GetComponent<SpriteRenderer> ().sprite;
		otherFriendOrig = otherFriend.GetComponent<SpriteRenderer> ().sprite;

		convoIndex1 = 0;

		conversationLines1 = new List<string> ();
		conversationLines1.Add ("I missed you guys so much!"); // rachel
		conversationLines1.Add ("Me too, I wish we could do this more often"); // friend 2
		conversationLines1.Add ("I really miss being able to go into the city with you guys"); // friend 1
		conversationLines1.Add ("We’ll have to do it again the next time we’re all in town"); // friend 2
		conversationLines1.Add ("Agreed"); // rachel
		conversationLines1.Add ("Alright, well I’ve got to get going. I have work in twenty"); // friend 2
		conversationLines1.Add ("Awww, okay. Bye, Maya!"); // friend 1
		conversationLines1.Add ("Bye, girl!"); // rachel
		conversationLines1.Add ("I'll see you guys soon, I promise!"); // friend 2

		heroText.enabled = false;
		friendText.enabled = false;
		friend2Text.enabled = false;
		moveText.enabled = false;

		stopWalking = false;
		friend2Leave = false;

		StartCoroutine (Cutscene ());
		independent = false;
	}

	void Update () {
		if (!cutBool) {
			if (!stopWalking) {
				GetComponent<Animator> ().enabled = true;
				hero.transform.position = Vector2.MoveTowards ((Vector2)hero.transform.position, new Vector2 (hero.transform.position.x + 12.5f, hero.transform.position.y), 0.1f);
				otherFriend.transform.position = Vector2.MoveTowards ((Vector2)otherFriend.transform.position, new Vector2 (otherFriend.transform.position.x + 12.5f, otherFriend.transform.position.y), 0.1f);
				// gameObject.transform.position = Vector2.MoveTowards ((Vector2)gameObject.transform.position, new Vector2 (gameObject.transform.position.x + 12.5f, gameObject.transform.position.y), 0.1f);
			} else if (stopWalking) {
				hero.GetComponent<Animator> ().enabled = false;
				hero.GetComponent<SpriteRenderer> ().sprite = heroOrig;
				GetComponent<Animator> ().enabled = false;
				GetComponent<SpriteRenderer> ().sprite = friendOrig;
				otherFriend.GetComponent<Animator> ().enabled = false;
				otherFriend.GetComponent<SpriteRenderer> ().sprite = otherFriendOrig;
			}
		}
		if (friend2Leave) {
			otherFriend.GetComponent<Animator> ().enabled = true;
			otherFriend.GetComponent<SpriteRenderer> ().flipX = true;
			otherFriend.transform.position = Vector2.MoveTowards ((Vector2)otherFriend.transform.position, new Vector2 (otherFriend.transform.position.x - 12.5f, otherFriend.transform.position.y), 0.1f);
			StartCoroutine (PressToMove());
			// Destroy (otherFriend); TODO needs to wait until the movement is complete and not destroy in the same frame...which it would
		}
	}

	public IEnumerator Cutscene () {

		yield return new WaitForSeconds (0.01f);
		p.CharacterPause = true;

		while (convoIndex1 < conversationLines1.Count) {
			if (convoIndex1 == 0) {
				tempText = heroText;
				tempBubble = heroBubble;
			} else if (convoIndex1 == 1) {
				tempText = friend2Text;
				tempBubble = friend2Bubble;
			} else if (convoIndex1 == 2) {
				tempText = friendText;
				tempBubble = friendBubble;
			} else if (convoIndex1 == 3) {
				tempText = friend2Text;
				tempBubble = friend2Bubble;
			} else if (convoIndex1 == 4) {
				tempText = heroText;
				tempBubble = heroBubble;
			} else if (convoIndex1 == 5) {
				tempText = friend2Text;
				tempBubble = friend2Bubble;
				stopWalking = true;
			} else if (convoIndex1 == 6) {
				tempText = friendText;
				tempBubble = friendBubble;
			} else if (convoIndex1 == 7) {
				tempText = heroText;
				tempBubble = heroBubble;
			} else {
				tempText = friend2Text;
				tempBubble = friend2Bubble;
			}

			tempText.text = conversationLines1 [convoIndex1]; 
			tempBubble.SetActive (true);
			tempText.enabled = true;
			yield return new WaitForSeconds (2.0f);
			tempBubble.SetActive (false);
			tempText.enabled = false;

			convoIndex1 = convoIndex1 + 1;
		}

		p.CharacterPause = false;
		cutBool = true;
		friend2Leave = true;

		// nothing happens until next event which happens at the player choice script???
	}
		
	public IEnumerator PressToMove () {
		yield return new WaitForSeconds (0.01f);
		moveText.enabled = true;
		while (playerNotMoving) {
			yield return new WaitForSeconds (0.01f);
			if (Input.GetKeyDown ("right") || Input.GetKeyDown ("d")) {
				playerNotMoving = false;
			}
		}
		moveText.enabled = false;
	}
}
