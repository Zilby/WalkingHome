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
	public GameObject stat;
	private bool stop;
	private float y;
	private Vector3 startPos;

	void Start () {
		counter = 120;
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
					GameObject p = Instantiate (stat);
					p.transform.position = new Vector3 (col.gameObject.transform.position.x - 0.5f, 
						col.gameObject.transform.position.y + 10, col.gameObject.transform.position.z + 0.5f);
					StatController ps = p.GetComponent<StatController> ();
					ps.change = "+1";
					ps.pickColor (1);

					counter = 120;
				} else {
					counter--;
				}
			}
		}
	}
		
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "hero") {
			GameController.paranoia += 10;
			GameController.confidence -= 5;

			GameObject c = Instantiate (stat);
			GameObject p = Instantiate (stat);

			Vector3 heroPos = col.gameObject.transform.position;

			c.transform.position = new Vector3 (heroPos.x + 0.25f, heroPos.y + 10, heroPos.z + 0.5f);
			p.transform.position = new Vector3 (heroPos.x - 0.25f, heroPos.y + 10, heroPos.z + 0.5f);

			StatController cs = c.GetComponent<StatController> ();
			StatController ps = p.GetComponent<StatController> ();

			cs.change = "-5";
			ps.change = "+10";

			cs.pickColor (0);
			ps.pickColor (1);

			transform.position = startPos;
		}
	} 

	void OnTriggerEnter (Collider col) {
		if (comment) {
			StartCoroutine (Catcall ());
		} 
		if (city) {
			stop = true;
			GameController.paranoia += 10;
			GameController.frustration += 15;
			GameController.confidence -= 5;

			GameObject f = Instantiate (stat);
			GameObject c = Instantiate (stat);
			GameObject p = Instantiate (stat);

			Vector3 heroPos = col.gameObject.transform.position;

			f.transform.position = new Vector3 (heroPos.x, heroPos.y + 10, heroPos.z + 0.5f);
			c.transform.position = new Vector3 (heroPos.x + 0.5f, heroPos.y + 10, heroPos.z + 0.5f);
			p.transform.position = new Vector3 (heroPos.x - 0.5f, heroPos.y + 10, heroPos.z + 0.5f);

			StatController fs = f.GetComponent<StatController> ();
			StatController cs = c.GetComponent<StatController> ();
			StatController ps = p.GetComponent<StatController> ();

			fs.change = "+15";
			cs.change = "-5";
			ps.change = "+10";

			fs.pickColor (2); 
			cs.pickColor (0);
			ps.pickColor (1);

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