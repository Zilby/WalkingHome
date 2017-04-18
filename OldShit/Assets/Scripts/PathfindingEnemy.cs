using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingEnemy : MonoBehaviour
{
	public GameObject hero;
	public int counter;
	private Vector3 startPos;

	void Start () {
		counter = 80;
		startPos = transform.position;
	}

	void Update () {
		transform.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = hero.transform.position;﻿

	}

	void OnTriggerStay(Collider col) {
		if (col.gameObject.tag == "hero") {
			if (counter == 0) {
				GameController.paranoia++;
				counter = 80;
			} else {
				counter--;
			}
		}
	}
		
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "hero") {
			GameController.paranoia += 20;
			transform.position = startPos;
		}
	} 
		

}