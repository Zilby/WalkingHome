﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// barebones movement for the maze, where animations don't apply
public class MazePlayerController : PlayerController {
	//public float moveSpeed;

	public GameObject collider;
	public GameObject thought;
	// public List<GameObject> initialThoughts;
	private float xDir;
	private float yDir;

	// Use this for initialization
	void Start () {
		hero = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> (); // gets the animator for the hero
		sr = GetComponent<SpriteRenderer> (); // gets the sprite renderer for the hero
		xDir = yDir = 0;
		orig = sr.sprite;
		characterPause = false;
		StartCoroutine (InitialThoughts ());
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (characterPause) {
			characterPauseMove (); // pauses the hero's movement 
		}
		else {
			MovePlayer (xDir, yDir); // moves the character
		}
	}

	void Update() {
		xDir = Input.GetAxisRaw ("Horizontal");
		yDir = Input.GetAxisRaw ("Vertical");
		if (GameController.paranoia > 50 && !paranoid) {
			StartCoroutine (Paranoid ());
			paranoid = true;
		}
		if ((GameController.frustration == 30 || GameController.frustration == 70) && !frustrate) {
			if (GameController.frustration == 30 && !f1) {
				f1 = true;
				StartCoroutine (Frustrated ());
				frustrate = true;
			} else if (GameController.frustration == 70 && !f2) {
				f2 = true;
				StartCoroutine (Frustrated ());
				frustrate = true;
			}
		}
	}

	private void MovePlayer(float horizontal, float vertical) {
		collider.transform.position = new Vector3 (collider.transform.position.x + (horizontal * moveSpeed), 
			collider.transform.position.y, collider.transform.position.z  + (vertical * moveSpeed));
		// determine animation displayed TODO vertical animations?
		if (Mathf.Abs (horizontal) >= 0.1 || Mathf.Abs (vertical) >= 0.1) {
			// walking animation for horizontal direction
			anim.enabled = true;
		} else {
			// idle animation
			stopAnimation();
			sr.sprite = orig;
		}
		if (horizontal < 0) {
			// face left
			sr.flipX = true;
		}
		if (horizontal > 0) {
			// face right
			sr.flipX = false;
		}
	}

	public IEnumerator Delay() {
		stopAnimation();
		characterPause = true;
		yield return new WaitForSeconds (0.4f);
		characterPause = false;
	}

	public IEnumerator Think() {
		thought.SetActive (true);
		yield return new WaitForSeconds (4f);
		thought.SetActive (true);
	}

	/* public IEnumerator InitialThoughts() {
		yield return new WaitForSeconds (1f);
		foreach (GameObject g in initialThoughts) {
			g.SetActive (true);
			yield return new WaitForSeconds (3.5f);
			g.SetActive (false);
		}
	} */ 
}
