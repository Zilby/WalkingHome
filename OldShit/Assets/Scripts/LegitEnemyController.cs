using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegitEnemyController : EnemyController {
	private bool first;
	public GameObject comment;
	private MazePlayerController player;

	void Start() {
		first = true;
	}

	public void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "hero") {
			player = col.gameObject.GetComponent<MazePlayerController> ();
			if(first) {
				player.CharacterPause = true; // sets the hero to pause
				StartCoroutine(Dialogue());
				player.stopAnimation (); // stops the animation of the hero
				first = false;
			}
		}
	}

	public IEnumerator Dialogue() {
		comment.SetActive (true); 
		yield return new WaitForSeconds (4);
		GameController.frustration += 5;
		GameController.paranoia += 2;
		GameController.confidence -= 1;
		comment.SetActive (false); 
		player.CharacterPause = false;
	}
}
