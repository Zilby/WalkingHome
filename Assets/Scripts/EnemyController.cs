using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour {

	public Transform[] waypoints; // array of points for the enemy to move between
	public GameObject comment;
	public SpriteRenderer sr;
	private int cur = 0;
	private Vector3 end;

	[SerializeField]
	private float moveSpeed; // reresents the movespeed of the enemy

    // Use this for initialization
	void Start () {
		if (waypoints.Length > 0) {
			end = waypoints [cur].position;
		}
    }

    // Update is called once per frame
    void FixedUpdate () {
		if (waypoints.Length > 0) {
			WaypointMove ();
		}
    }

	void WaypointMove() {
		if (Vector3.Distance(transform.position, waypoints [cur].position) > 0.01f) {
			transform.position = Vector3.MoveTowards (transform.position, end, moveSpeed * Time.deltaTime);
			end = Vector3.MoveTowards (end, transform.position, moveSpeed * -1.0f * Time.deltaTime);
		}
		// Waypoint reached, select next one
		else { 
			cur = (cur + 1) % waypoints.Length;
			end = waypoints [cur].position;
			if (end.x < transform.position.x) {
				sr.flipX = true;
			} else {
				sr.flipX = false;
			}
		}
	}
		
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "hero") {
			if(comment) {
				StartCoroutine (Catcall ());
			} else {
				Destroy (gameObject);
			}
		}
	}

	public IEnumerator Catcall() {
		if (!comment.activeInHierarchy) {
			comment.SetActive (true);
			GameController.frustration += 15;
			GameController.confidence -= 10;
			GameController.paranoia += 10;
			yield return new WaitForSeconds (3.0f);
			comment.SetActive (false);
		}
	}
}

