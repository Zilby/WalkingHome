using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StaticEnemyController : MonoBehaviour {
	public ThoughtsController thoughts;
	public PlayerController player;
	private bool first;

	void Start() {
		first = true;
	}

	// function for when the hero collides with this enemy
	void OnTriggerEnter2D(Collider2D other)
	{
		if (first) {
			player.CharacterPause = true; // sets the hero to pause
			other.GetComponent<PlayerController>().stopAnimation (); // stops the animation of the hero
			StartCoroutine(thoughts.EdgelordThoughts());
			first = false;
		}
	}
}
