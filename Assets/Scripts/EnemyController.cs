using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public Transform[] waypoints; // array of points for the enemy to move between
	public GameObject comment;
	public static List<string> comments = new List<string>();
	public List<GameObject> responses;
	public SpriteRenderer sr;
	public GameObject stat;
	public bool endScroll = false;
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

		comments.Add ("How much, baby?");
		comments.Add ("Damn girl, you’ve got some sexy legs.");
		comments.Add ("Screw the gym, baby, I’ll give you a workout.");
		comments.Add ("*kissing noises*");
		comments.Add ("Whew, look at that ass!");
		comments.Add ("Let’s see a smile, baby.");
		comments.Add ("Dayummm, girl!");
		comments.Add ("*whistle*");
		comments.Add ("Hey there, sexy mama. Can I get your number?");
		comments.Add ("Can I take you home?");
		comments.Add ("Hot damn, I could feast on you.");
		comments.Add ("Take care of that ass, sweetie.");
		comments.Add ("Damn baby, let me give you a ride home. My car’s only a block away.");
		comments.Add ("You are fine, sexy lady.");
		comments.Add ("You want some fries with that shake, baby girl?");
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
			comment.GetComponentInChildren<Text> ().text = comments[Random.Range (0, comments.Count)];
			comment.SetActive (true);

			if (!endScroll) {
				GameController.frustration += 10;
				GameController.confidence -= 5;
				GameController.paranoia += 5;

				GameObject f = Instantiate (stat);
				GameObject c = Instantiate (stat);
				GameObject p = Instantiate (stat);

				f.transform.position = new Vector3 (heroPos.x, heroPos.y + 10, heroPos.z + 0.5f);
				c.transform.position = new Vector3 (heroPos.x + 0.5f, heroPos.y + 10, heroPos.z + 0.5f);
				p.transform.position = new Vector3 (heroPos.x - 0.5f, heroPos.y + 10, heroPos.z + 0.5f);

				StatController fs = f.GetComponent<StatController> ();
				StatController cs = c.GetComponent<StatController> ();
				StatController ps = p.GetComponent<StatController> ();

				fs.change = "+10";
				cs.change = "-5";
				ps.change = "+5";

				fs.pickColor (2); 
				cs.pickColor (0);
				ps.pickColor (1);

			}/* else {
				yield return new WaitForSeconds (0.2f);

				GameObject f = Instantiate (stat);
				GameObject c = Instantiate (stat);
				GameObject p = Instantiate (stat);

				f.transform.position = new Vector3 (heroPos.x, heroPos.y + 1.5f, heroPos.z - 5f);
				c.transform.position = new Vector3 (heroPos.x + 1.5f, heroPos.y + 1.5f, heroPos.z - 5f);
				p.transform.position = new Vector3 (heroPos.x - 1.5f, heroPos.y + 1.5f, heroPos.z - 5f);

				StatController fs = f.GetComponent<StatController> ();
				StatController cs = c.GetComponent<StatController> ();
				StatController ps = p.GetComponent<StatController> ();

				fs.change = "+10";
				cs.change = "-5";
				ps.change = "+5";

				fs.pickColor (2); 
				cs.pickColor (0);
				ps.pickColor (1);

			}*/

			yield return new WaitForSeconds (3.0f);
			comment.SetActive (false);
			GameObject r = responses [Random.Range (0, responses.Count)];
			r.SetActive (true);
			yield return new WaitForSeconds (3.0f);
			r.SetActive (false);
		}
	}

	public IEnumerator Catcall2() {
		yield return new WaitForSecondsRealtime (0.01f);
		comment.SetActive (false);

		comment.GetComponentInChildren<Text> ().text = comments[Random.Range (0, comments.Count)];
		comment.SetActive (true);
		yield return new WaitForSecondsRealtime (1.5f);
		comment.SetActive (false);
	}
}

