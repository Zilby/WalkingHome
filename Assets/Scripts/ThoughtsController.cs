using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtsController : MonoBehaviour {

	public GameObject player;
	public GameObject thoughtBubble;
	public GameObject catcall;
	public GameObject frustration;
	public GameObject paranoia;
	private Text text; // the thought to be displayed
	List<string> edgyThoughts;
	int edgyIndex;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>(); // gets the thought to be displayed
		edgyIndex = 0;
		edgyThoughts = new List<string> ();
		edgyThoughts.Add ("Wow, that was creepy...");
		edgyThoughts.Add ("And frustrating...");
		edgyThoughts.Add ("He’s not following me, right?");
		edgyThoughts.Add ("Like, how was I even supposed to respond to that?");
		edgyThoughts.Add ("It’s like, if I defend myself, I’m risking more harassment... or worse...");
		edgyThoughts.Add ("But if I don’t, am I just letting him win?");
		edgyThoughts.Add ("God, this is exactly why I hate Manhattan."); 
		edgyThoughts.Add ("I’m just gonna keep my head down and walk fast.");
		edgyThoughts.Add ("Penn Station can’t be too far away.");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator EdgelordThoughts() {
		player.GetComponent<Animator> ().enabled = false;
		catcall.SetActive (true); 
		yield return new WaitForSeconds (5.0f);
		catcall.SetActive (false);
		GameController.frustration += 5;
		GameController.paranoia += 2;
		GameController.confidence -= 1;
		frustration.SetActive (true);
		paranoia.SetActive (true);

		thoughtBubble.SetActive (true);
		text.enabled = true;

		player.GetComponent<PlayerController> ().CharacterPause = false;
		player.GetComponent<Animator> ().enabled = false;

		while (edgyIndex < edgyThoughts.Count) {
			text.text = edgyThoughts [edgyIndex];
			yield return new WaitForSeconds (2.5f);
			edgyIndex += 1;
		}

		text.enabled = false;
		thoughtBubble.SetActive (false);
	}
		
}