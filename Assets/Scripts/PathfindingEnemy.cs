using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingEnemy : MonoBehaviour
{
	public GameObject hero;
	/*
	private float diffx;
	private float diffy;
	private Collision2D c;
	public bool[][] grid  = new bool[35][19];
	public GameObject tester;
	private int currx;
	private int curry;
	private int gotox;
	private int gotoy;
	*/
	private readonly float gap = 0.218f;
	//public GameObject enemy;

	private float moveSpeed;

	private Rigidbody2D enemyBody; 

	public LayerMask lm;

	public bool touchingWall;

	//public GameObject endPos;

	void Start () {
		enemyBody = gameObject.GetComponent<Rigidbody2D> ();
		moveSpeed = .01f;
		lm = 9;
		touchingWall = false;
	}

	void Update () {
		//transform.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = hero.transform.position;﻿
		/*
		diffx = hero.transform.position.x - transform.position.x;
		diffy = hero.transform.position.y - transform.position.y;
		if (Mathf.Abs (diffx) > Mathf.Abs (diffy)) {
			if (diffx > 0) {
				gotox = currx + 1;
			} else {
				gotox = currx - 1;
			}
		} else {
			if (diffy > 0) {
				gotoy = curry + 1;
			} else {
				gotoy = curry - 1;
			}
		} 
		if (gotox < 0 || gotoy < 0 || gotox >= coord.Count || gotoy >= coord [0].Count || !coord [gotox] [gotoy]) {
			if (Mathf.Abs (diffx) > Mathf.Abs (diffy)) {
				if (diffy > 0) {
					gotoy = curry + 1;
				} else {
					gotoy = curry - 1;
				}
			} else {
				if (diffx > 0) {
					gotox = currx + 1;
				} else {
					gotox = currx - 1;
				}
			}
		}
		if (gotox < 0 || gotoy < 0 || gotox >= coord.Count || gotoy >= coord [0].Count || !coord [gotox] [gotoy]) {
			if (Mathf.Abs (diffx) > Mathf.Abs (diffy)) {
				if (diffy > 0) {
					gotoy = curry - 1;
				} else {
					gotoy = curry + 1;
				}
			} else {
				if (diffx > 0) {
					gotox = currx - 1;
				} else {
					gotox = currx + 1;
				}
			}
		}
		/*
		//Debug.DrawLine (transform.position, hero.transform.position);
		//Debug.Log (Physics2D.Raycast (transform.position, hero.transform.position, Vector2.Distance(transform.position, hero.transform.position), lm.value).collider);
		//if (Physics2D.Raycast (transform.position, hero.transform.position, Vector2.Distance(transform.position, hero.transform.position), lm.value).collider == null) {
		if (!touchingWall) {
			if (Mathf.Abs (diffx) > Mathf.Abs (diffy)) {
				if (diffx > 0) {
					transform.position = new Vector2 (transform.position.x + 1 * moveSpeed, transform.position.y);
				} else {
					transform.position = new Vector2 (transform.position.x - 1 * moveSpeed, transform.position.y);
				}
			} else {
				if (diffy > 0) {
					transform.position = new Vector2 (transform.position.x, transform.position.y + 1 * moveSpeed);
				} else {
					transform.position = new Vector2 (transform.position.x, transform.position.y - 1 * moveSpeed);
				}
			}
		} else {
			MoveWall();
		}
		*/
		//} else {
		//	MoveDown ();
		//}
		// Debug.Log ("wall-detection");

	}


	public void init() {
		
		/*
		grid [0] = new bool[] { true, true, true, true, true, false, true, true, 
			true, true, true, false, true, true, true, true, true, true, true };
		grid [35] = new bool[] { true, true, true, true, true, false, true, true, 
			true, true, true, false, true, true, true, true, true, true, true };
		grid [1] = new bool[] { true, false, false, false, true, false, true, false, 
			false, false, true, false, true, false, false, false, true, false, true };
		grid [34] = new bool[] { true, false, false, false, true, false, true, false, 
			false, false, true, false, true, false, false, false, true, false, true };
		grid [2] = new bool[] { true, true, true, false, true, true, true, true, 
			true, true, true, true, true, true, true, true, true, false, true };
		grid [33] = new bool[] { true, true, true, false, true, true, true, true, 
			true, true, true, true, true, true, true, true, true, false, true };
		grid [3] = new bool[] { true, false, false, false, true, false, true, false, 
			false, false, true, true,   true, false, false, false, true, false, true };
			*/

	}

	void MoveWall() {
		/*
		float diffx2 = c.contacts[0].point.x - transform.position.x;
		float diffy2 = c.contacts[0].point.y - transform.position.y;
		if (Mathf.Abs (diffx2) > Mathf.Abs (diffy2)) {
			if (diffy > 0) {
				transform.position = new Vector2 (transform.position.x, transform.position.y + 1 * moveSpeed);
			} else {
				transform.position = new Vector2 (transform.position.x, transform.position.y - 1 * moveSpeed);
			}
		} else {
			if (diffx > 0) {
				transform.position = new Vector2 (transform.position.x + 1 * moveSpeed, transform.position.y);
			} else {
				transform.position = new Vector2 (transform.position.x - 1 * moveSpeed, transform.position.y);
			}
		}
		*/
	}
	/*
	void MoveEnemy () {
		// Debug.Log ("working hard");
		Vector2 p = Vector2.MoveTowards (transform.position,
			hero.transform.position,
			moveSpeed);
		enemyBody.MovePosition (p);
	}
	*/

	void OnCollisionEnter2D (Collision2D col) {
		/*Vector2 p = Vector2.MoveTowards(transform.position,
			hero.transform.position,
			2);
		if (Physics2D.Raycast (transform.position, p, 2, lm.value).collider != null) {
			Debug.Log ("damn wall");
			Debug.Log (Physics2D.Raycast (transform.position, p, 2, lm.value).collider);
			Debug.DrawLine (transform.position, p, Color.red);
			transform.position = new Vector2 (transform.position.x, transform.position.y - (1 * moveSpeed));
		} else {
			Debug.Log ("hurrah");
			Debug.Log (Physics2D.Raycast (transform.position, p, 2, lm.value).collider);
			Debug.DrawLine (transform.position, p, Color.red);
			MoveEnemy ();
		}*/
		// transform.position = new Vector2 (transform.position.x, transform.position.y - 1 * moveSpeed);
		//c = col;
		touchingWall = true;
	} 

	void OnCollisionExit2D(Collision2D col) {
		touchingWall = false;
	}

	void MoveDown () {
		transform.position = new Vector2 (transform.position.x, transform.position.y - 1 * moveSpeed);
	}

}