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

	private PlayerController p; // to pause the player's movement
	// public Animator anim; // animator for the hero?

	List<string> conversationLines1; // lines for the first conversation between three friends
	List<string> conversationLines2; // lines for the conversation between two friends

	private int convoIndex1;
	private bool cutBool;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1.0f;
		p = hero.GetComponent<PlayerController> ();
		p.CharacterPause = true;
		// anim.enabled = false;
		convoIndex1 = 0;

		conversationLines1 = new List<string> ();
		conversationLines1.Add ("I missed you guys so much!"); // rachel
		conversationLines1.Add ("Me too, I wish we could do this more often."); // friend 2
		conversationLines1.Add ("I really miss being able to go into the city with you guys."); // friend 1
		conversationLines1.Add ("We’ll have to do it again the next time we’re all in town."); // friend 2
		conversationLines1.Add ("Agreed."); // rachel
		conversationLines1.Add ("Alright, well I’ve got to get going. I have work in twenty."); // friend 2
		conversationLines1.Add ("Awww, okay. Bye, Maya!"); // friend 1
		conversationLines1.Add ("Bye, girl!"); // rachel
		conversationLines1.Add ("I'll see you guys soon, I promise!"); // friend 2

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
		StartCoroutine (Cutscene ());
	}

	/*void Update () {
		if (!cutBool) {
			
		}
		Debug.Log ("Misery");
	}*/

	public IEnumerator Cutscene () {
		yield return new WaitForSeconds (0.01f);
		p.CharacterPause = true;
		Debug.Log ("before the while");
		while (convoIndex1 < conversationLines1.Count) {
			if (convoIndex1 == 0) {
				Debug.Log (convoIndex1);
				tempText = heroText;
				tempBubble = heroBubble;
				/*heroText.text = conversationLines1 [convoIndex1]; 
				heroBubble.SetActive (true);
				heroText.enabled = true;
				heroBubble.SetActive(false);
				heroText.enabled = false;*/
			} else if (convoIndex1 == 1) {
				Debug.Log (convoIndex1);
				tempText = friend2Text;
				tempBubble = friend2Bubble;
				//friend2Text.text = conversationLines1 [convoIndex1]; 
				/*friend2Bubble.SetActive (true);
				friend2Text.enabled = true;
				friend2Bubble.SetActive(false);
				friend2Text.enabled = false;*/
			} else if (convoIndex1 == 2) {
				Debug.Log (convoIndex1);
				tempText = friendText;
				tempBubble = friendBubble;
				/*friendText.text = conversationLines1 [convoIndex1]; 
				friendBubble.SetActive (true);
				friendText.enabled = true;
				friendBubble.SetActive(false);
				friendText.enabled = false;*/
			} else if (convoIndex1 == 3) {
				Debug.Log (convoIndex1);
				tempText = friend2Text;
				tempBubble = friend2Bubble;
				/*friend2Text.text = conversationLines1 [convoIndex1]; 
				friend2Bubble.SetActive (true);
				friend2Text.enabled = true;
				friend2Bubble.SetActive(false);
				friend2Text.enabled = false;*/
			} else if (convoIndex1 == 4) {
				Debug.Log (convoIndex1);
				tempText = heroText;
				tempBubble = heroBubble;
				/*heroText.text = conversationLines1 [convoIndex1]; 
				heroBubble.SetActive (true);
				heroText.enabled = true;
				heroBubble.SetActive(false);
				heroText.enabled = false;*/
			} else if (convoIndex1 == 5) {
				Debug.Log (convoIndex1);
				tempText = friend2Text;
				tempBubble = friend2Bubble;
				/*friend2Text.text = conversationLines1 [convoIndex1]; 
				friend2Bubble.SetActive (true);
				friend2Text.enabled = true;
				friend2Bubble.SetActive(false);
				friend2Text.enabled = false;*/
			} else if (convoIndex1 == 6) {
				Debug.Log (convoIndex1);
				tempText = friendText;
				tempBubble = friendBubble;
				/*friendText.text = conversationLines1 [convoIndex1]; 
				friendBubble.SetActive (true);
				friendText.enabled = true;
				yield return new WaitForSeconds (1.0f);
				friendBubble.SetActive(false);
				friendText.enabled = false;*/
			} else if (convoIndex1 == 7) {
				Debug.Log (convoIndex1);
				tempText = heroText;
				tempBubble = heroBubble;
				/* heroText.text = conversationLines1 [convoIndex1]; 
				heroBubble.SetActive (true);
				heroText.enabled = true;
				heroBubble.SetActive(false);
				heroText.enabled = false; */
			} else {
				Debug.Log (convoIndex1);
				tempText = friend2Text;
				tempBubble = friend2Bubble;
				/*friend2Text.text = conversationLines1 [convoIndex1]; 
				friend2Bubble.SetActive (true);
				friend2Text.enabled = true;
				friend2Bubble.SetActive(false);
				friend2Text.enabled = false;*/
			}
			tempText.text = conversationLines1 [convoIndex1]; 
			tempBubble.SetActive (true);
			tempText.enabled = true;
			yield return new WaitForSeconds (1.0f);
			tempBubble.SetActive (false);
			tempText.enabled = false;
			convoIndex1 = convoIndex1 + 1;
		}
		Debug.Log ("end while loop");
		p.CharacterPause = false;
		cutBool = true;
		// nothing happens until next event which happens at the player choice script???
	}
	/* public IEnumerator DialogueOptions () {
		Debug.Log ("we started the options");
		thinking.SetActive(true);

		while (heroText.text != "Option one" && heroText.text != ("Option two")) {
			yield return new WaitForSeconds (.01f);
			if (Input.GetKeyDown ("1")) {
				thinking.SetActive(false);
				heroText.text = "Option one";
				Debug.Log ("Option one selected");
			} else if (Input.GetKeyDown ("2")) {
				thinking.SetActive(false);
				heroText.text = "Option two";
				Debug.Log ("Option two selected");
			}
		}
		heroBubble.SetActive(true);
		heroText.enabled = true;
		yield return new WaitForSeconds (2.0f);
		heroBubble.SetActive(false);
		heroText.enabled = false;
	}*/
}
