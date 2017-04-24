using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingEnemy : MonoBehaviour
{
	public GameObject hero;
	private int counter;
	public bool city;
	public GameObject comment;
	public SpriteRenderer sr;
	private bool stop;
	private float y;
	private Vector3 startPos;

	void Start () {
		counter = 80;
		startPos = transform.position;
		stop = false;
		y = transform.position.y;
	}

	void Update () {
		if (!stop) {
			transform.GetComponent<UnityEngine.AI.NavMeshAgent> ().destination = new Vector3(hero.transform.position.x, hero.transform.position.y, hero.transform.position.z - 0.2f);﻿
			if (city) {
				transform.position = new Vector3 (transform.position.x, y, transform.position.z);
			}
		}
		if (sr) {
			if (transform.GetComponent<UnityEngine.AI.NavMeshAgent> ().velocity.x > 0) {
				sr.flipX = false;
			} else {
				sr.flipX = true;
			}
		}
	}

	void OnTriggerStay(Collider col) {
		if (!city) {
			if (col.gameObject.tag == "hero") {
				if (counter == 0) {
					GameController.paranoia++;
					counter = 80;
				} else {
					counter--;
				}
			}
		}
	}
		
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "hero") {
			GameController.paranoia += 20;
			GameController.confidence -= 5;
			transform.position = startPos;
		}
	} 

	void OnTriggerEnter (Collider col) {
		if (comment) {
			StartCoroutine (Catcall ());
		} 
		if (city) {
			stop = true;
			GameController.paranoia += 20;
			GameController.frustration += 25;
			GameController.confidence -= 10;
			sr.gameObject.GetComponent<Animator> ().enabled = false;
			gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
		}
	}
		
	public IEnumerator Catcall() {
		if (!comment.activeInHierarchy && !stop) {
			comment.SetActive (true);
			yield return new WaitForSeconds (4.0f);
			comment.SetActive (false);
		}
	}
}