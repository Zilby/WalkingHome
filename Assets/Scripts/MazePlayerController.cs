using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// barebones movement for the maze, where animations don't apply
public class MazePlayerController : MonoBehaviour {

	private Rigidbody2D hero;
	public SpriteRenderer sr;

	[SerializeField]
	private float moveSpeed;

	public int paranoia; 
	public int confidence;
	public int frustration;    

	// Use this for initialization
	void Start () {
		hero = GetComponent<Rigidbody2D> ();
		paranoia = 0;
		confidence = 10;
		frustration = 0;
	}

	// Update is called once per frame
	void Update () {
		float xDir = Input.GetAxis("Horizontal");
		float yDir = Input.GetAxis("Vertical");

		MovePlayer(xDir, yDir);
	}

	private void MovePlayer(float horizontal, float vertical) {
		hero.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
	}
}
