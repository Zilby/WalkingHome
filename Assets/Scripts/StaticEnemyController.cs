	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StaticEnemyController : MonoBehaviour {
	public GameObject hero;
	public ThoughtsController thoughts;
	public PlayerController player;
	private bool first;
	private Sprite spr;

	void Start() {
		first = true;
		spr = hero.GetComponent<Sprite> ();
	}

	// function for when the hero collides with this enemy
	void OnTriggerEnter2D(Collider2D other)
	{
		if (first) {
			player.CharacterPause = true; // sets the hero to pause
			player.stopAnimation ();
			hero.GetComponent<SpriteRenderer> ().sprite = spr;
			StartCoroutine(thoughts.EdgelordThoughts());
			first = false;
		}
	}
}
