using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StaticEnemyController : MonoBehaviour {

	bool startTimer; // determines if the timer should be started
	int timer = 0; // initializes the timer to 0

	// Use this for initialization
	void Start () {
		startTimer = false; // sets the timer to be turned off
	}

	// Update is called once per frame
	void Update () {
		if (startTimer) { // if the timer is started
			timer += 1; // increment the timer
		}
		if (timer >= 100) { // once the timer equals or exceeds 1000
			PlayerController.characterPause = false; // the hero may continue to move again
			startTimer = false; // turns the timer off
			timer = 0; // resets the timer
		}
	}

	// function for when the hero collides with this enemy
	void OnTriggerEnter2D(Collider2D other)
	{
		PlayerController.characterPause = true; // sets the hero to pause
		PlayerController.stopAnimation (); // stops the animation of the hero
		startPlayerMovementCounter (); // starts the timer
	}

	// starts the timer
	void startPlayerMovementCounter() {
		startTimer = true; // starts the timer
	}
}
