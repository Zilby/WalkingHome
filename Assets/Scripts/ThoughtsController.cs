using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtsController : MonoBehaviour {

	public GameObject player;
	public GameObject catcall;
	public GameObject frustration;
	public GameObject paranoia;
	private Text text; // the thought to be displayed
	List<string> firstThoughts; 
	List<string> edgyThoughts; 
	int firstIndex;
	int edgyIndex;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>(); // gets the thought to be displayed
		firstIndex = 0;
		edgyIndex = 0;
		firstThoughts = new List<string> ();
		edgyThoughts = new List<string> ();
		firstThoughts.Add ("I’m new to the city, and I’m not the best with directions");
		firstThoughts.Add ("But I know my way back to my apartment from my friend’s apartment, so I’m not too worried");
		edgyThoughts.Add ("Hey— he’s kind of cute");
		edgyThoughts.Add ("He’s smiling at me. I guess I’ll smile back");
		edgyThoughts.Add ("But... I don’t like the way he’s looking at me right now");
		edgyThoughts.Add ("Is he staring at my body...?");
		edgyThoughts.Add ("Did he just— did he really just say that?");
		text.text = firstThoughts [firstIndex];
	}
	
	// Update is called once per frame
	void Update () {
		if ((firstIndex == 0 && player.transform.position.x > 2)
			//(index == 1 && player.transform.position.x > 2)
			//(index == 2 && player.transform.position.x > 13)
			//(index == 3 && player.transform.position.x > 24)
		) {
			firstIndex++;
			text.text = firstThoughts [firstIndex];
		}
	}

	public IEnumerator EdgelordThoughts() {
		while (edgyIndex < edgyThoughts.Count - 1) {
			text.text = edgyThoughts [edgyIndex];
			yield return new WaitForSeconds (4);
			edgyIndex++;
		}
		text.enabled = false; 
		catcall.SetActive (true); 
		yield return new WaitForSeconds (5);
		PlayerController.frustration += 5;
		PlayerController.paranoia += 2;
		PlayerController.confidence -= 1;
		frustration.SetActive (true);
		paranoia.SetActive (true);
		text.enabled = true; 
		catcall.SetActive (false); 
		text.text = edgyThoughts [edgyIndex];
		player.GetComponent<PlayerController> ().CharacterPause = false;
	}
		
}