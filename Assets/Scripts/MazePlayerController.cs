using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// barebones movement for the maze, where animations don't apply
public class MazePlayerController : MonoBehaviour {

	public float moveSpeed;

	public int paranoia; 
	public int confidence;
	public int frustration; 

	private float xDir;
	private float yDir;

	private static Sprite orig;
	public static SpriteRenderer sr; // the hero's sprite renderer
	static Animator anim; // controls the animations for this hero

	// Use this for initialization
	void Start () {
		//hero = GetComponent<Rigidbody2D> ();
		paranoia = 0;
		confidence = 10;
		frustration = 0;
		anim = GetComponent<Animator> (); // gets the animator for the hero
		sr = GetComponent<SpriteRenderer> (); // gets the sprite renderer for the hero
		xDir = yDir = 0;
		orig = sr.sprite;
	}

	// Update is called once per frame
	void FixedUpdate () {
		MovePlayer(xDir, yDir);
	}

	public void Update() {
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

	// stops the hero's movement animation
	public void stopAnimation() {
		StartCoroutine(PauseAnim());
	}

	private IEnumerator PauseAnim() {
		yield return new WaitForSeconds (0.00000001f);
		while (sr.sprite != orig) {
			yield return new WaitForSeconds (0.00000001f);
		}
		anim.enabled = false;
	}
}
