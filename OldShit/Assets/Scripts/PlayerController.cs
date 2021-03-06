﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    protected Rigidbody2D hero; // the hero's physics body
	public SpriteRenderer sr; // the hero's sprite renderer
	protected bool characterPause; // determines if the character will be stopped
	protected Sprite orig;

    public float moveSpeed; // determines the moveSpeed of the hero


	protected int counter = 0; // used to modify meters

	protected Animator anim; // controls the animations for this hero

    //-Paranoia/anxiety: If paranoia/anxiety reaches its max capacity, speed increases? amount of blobs increases?
    //-Confidence:       If confidence reaches its minimum capacity, the player’s paranoia/anxiety and frustration meters
    //                   are impacted significantly more by attacks of harassment until the player can regain their
    //                   confidence.
    //-Frustration:      

    // Use this for initialization
    void Start () {
        hero = GetComponent<Rigidbody2D> (); // gets the physics body of the hero
		anim = GetComponent<Animator> (); // gets the animator for the hero
		sr = GetComponent<SpriteRenderer> (); // gets the sprite renderer for the hero

		characterPause = false; // initializes character to not be stopped
		orig = sr.sprite;
	}
	
	// Update is called once per frame
	void Update () {
        float xDir = Input.GetAxis("Horizontal"); // reads horizontal inputs from the keyboard
        float yDir = Input.GetAxis("Vertical"); // reads vertical inputs from the keyboard

		if (characterPause) {
			characterPauseMove (); // pauses the hero's movement 
		}
		else {
			MovePlayer (xDir, yDir); // moves the character
		}
	}

	// moves the player, given two input floats
    private void MovePlayer(float horizontal, float vertical) {
		//determine sprite flipping
		if (horizontal < 0) {
			// face left
			sr.flipX = true;
		}
		if (horizontal > 0) {
			// face right
			sr.flipX = false;
		}

		// determine animation displayed TODO vertical animations?
		if (Mathf.Abs (horizontal) >= 0.1) {
			// walking animation for horizontal direction
			anim.enabled = true;
		} else {
			// idle animation
			stopAnimation();
			sr.sprite = orig;
		}

		// actual movement for the hero occurs here by applying a change to the hero's velocity
        hero.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }

	// causes the hero to stop moving
	protected void characterPauseMove () {
		hero.velocity = new Vector2 (0, 0);
	}

	// stops the hero's movement animation
	public void stopAnimation() {
		StartCoroutine(PauseAnim());
	}

	protected IEnumerator PauseAnim() {
		yield return new WaitForSeconds (0.00000001f);
		while (sr.sprite != orig) {
			yield return new WaitForSeconds (0.00000001f);
		}
		anim.enabled = false;
	}

	public bool CharacterPause
	{
		get { return characterPause; }
		set { characterPause = value; }
	}
}
