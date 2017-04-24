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
	public GameObject stat;
	private Text text; // the thought to be displayed
	private List<string> edgyThoughts;
	private int edgyIndex;
	private bool catCallDone;
	private Sprite heroOrig;

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
		edgyThoughts.Add ("God, this is exactly why I hate Manhattan"); 
		edgyThoughts.Add ("I’m just gonna keep my head down and walk fast");
		edgyThoughts.Add ("Penn Station can’t be too far away");
		catCallDone = true;
		heroOrig = player.GetComponent<SpriteRenderer> ().sprite;
	}

	// Update is called once per frame
	void Update () {
		if (!catCallDone) {
			player.GetComponent<Animator> ().enabled = false;
			player.GetComponent<SpriteRenderer> ().sprite = heroOrig;
		}
	}

	public IEnumerator EdgelordThoughts() {
		yield return new WaitForSeconds (0.000001f);
		catCallDone = false;

		catcall.SetActive (true); 
		yield return new WaitForSeconds (5.0f);
		catcall.SetActive (false);

		catCallDone = true;

		GameController.frustration += 20;
		GameController.paranoia += 5;
		GameController.confidence -= 5;
		frustration.SetActive (true);
		paranoia.SetActive (true);

		GameObject f = Instantiate (stat);
		GameObject c = Instantiate (stat);
		GameObject p = Instantiate (stat);

		Vector3 heroPos = player.transform.position;

		f.transform.position = new Vector3 (heroPos.x, heroPos.y + 1.5f, heroPos.z - 5f);
		c.transform.position = new Vector3 (heroPos.x + 1.5f, heroPos.y + 1.5f, heroPos.z - 5f);
		p.transform.position = new Vector3 (heroPos.x - 1.5f, heroPos.y + 1.5f, heroPos.z - 5f);

		StatController fs = f.GetComponent<StatController> ();
		StatController cs = c.GetComponent<StatController> ();
		StatController ps = p.GetComponent<StatController> ();

		fs.change = "+20";
		cs.change = "-5";
		ps.change = "+5";

		fs.pickColor (2); 
		cs.pickColor (0);
		ps.pickColor (1);


		player.GetComponent<PlayerController> ().CharacterPause = false;
		// player.GetComponent<Animator> ().enabled = true;

		thoughtBubble.SetActive (true);
		text.enabled = true;

		while (edgyIndex < edgyThoughts.Count) {
			text.text = edgyThoughts [edgyIndex];
			yield return new WaitForSeconds (2.5f);
			edgyIndex += 1;
		}

		text.enabled = false;
		thoughtBubble.SetActive (false);
	}
		
}