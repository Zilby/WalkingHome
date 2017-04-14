﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// barebones movement for the maze, where animations don't apply
public class MazePlayerController : PlayerController {
	//public float moveSpeed;

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
	}

	private void MovePlayer(float horizontal, float vertical) {
		transform.position = new Vector3 (transform.position.x + (horizontal * moveSpeed), 
			transform.position.y + (vertical * moveSpeed), transform.position.z);
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
}
