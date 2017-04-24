using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this one goes out to all the boys and girls out there who don't know how naming conventions work for game programming
// -- much love, another programming-challenged coder
public class StatController : MonoBehaviour {

	public float speed = 1;
	public int stat; 
	public string change;
	public bool y;

	private Color textColor;

	// Use this for initialization
	void Start () {
		StartCoroutine (Fade ());
	}

	void FixedUpdate () {
		if(y) {
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, 
				new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z), speed / 10f * Time.deltaTime);
		} else {
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, 
				new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 5), speed / 10f * Time.deltaTime);
		}
		GetComponent<Text> ().text = change;
		if (GetComponent<CanvasRenderer>().GetColor().a <= 0.05) { // TODO determine actual position 
			Destroy(gameObject);
		}
	}

	public void pickColor(int statType) {
		if (statType == 0) {
			// set color to confidence
			GetComponent<Text> ().color = new Color(0f, 0.6f, 1f);
		} else if (statType == 1) {
			// set color to paranoia
			GetComponent<Text> ().color = Color.yellow;
		} else {
			// set color to frustration
			GetComponent<Text> ().color = Color.red;
		}
	}

	private IEnumerator Fade() {
		yield return new WaitForSeconds (1.5f); 
		GetComponent<Text> ().CrossFadeAlpha (0, 1f, false);
	}
}
