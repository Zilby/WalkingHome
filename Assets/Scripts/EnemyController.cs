using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour {

	public Transform[] waypoints; // array of points for the enemy to move between
	public GameObject comment;
	public List<GameObject> responses;
	public SpriteRenderer sr;
	public GameObject stat;
	private int cur = 0;
	private Vector3 end;
	private Vector3 heroPos;

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
		if (Vector3.Distance(transform.position, waypoints [cur].position) > 0.02f) {
			transform.position = Vector3.MoveTowards (transform.position, end, moveSpeed * Time.deltaTime);
			end = Vector3.MoveTowards (end, transform.position, moveSpeed * -1.0f * Time.deltaTime);
		}
		// Waypoint reached, select next one
		else { 
			cur = (cur + 1) % waypoints.Length;
			end = waypoints [cur].position;
			if (sr) {
				if (end.x < transform.position.x) {
					sr.flipX = true;
				} else {
					sr.flipX = false;
				}
			}
		}
	}
		
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "hero") {
			if(comment) {
				heroPos = col.gameObject.transform.position;
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

			GameObject f = Instantiate (stat);
			GameObject c = Instantiate (stat);
			GameObject p = Instantiate (stat);

			f.transform.position = new Vector3 (heroPos.x, heroPos.y + 10, heroPos.z + 0.5f);
			c.transform.position = new Vector3 (heroPos.x + 0.5f, heroPos.y + 10, heroPos.z + 0.5f);
			p.transform.position = new Vector3 (heroPos.x - 0.5f, heroPos.y + 10, heroPos.z + 0.5f);

			StatController fs = f.GetComponent<StatController> ();
			StatController cs = c.GetComponent<StatController> ();
			StatController ps = p.GetComponent<StatController> ();

			fs.change = "+15";
			cs.change = "-10";
			ps.change = "+10";

			fs.pickColor (2); 
			cs.pickColor (0);
			ps.pickColor (1);


			yield return new WaitForSeconds (3.0f);
			comment.SetActive (false);
			GameObject r = responses [Random.Range (0, responses.Count)];
			r.SetActive (true);
			yield return new WaitForSeconds (3.0f);
			r.SetActive (false);
		}
	}
}

