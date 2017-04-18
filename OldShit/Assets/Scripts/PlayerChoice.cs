using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChoice : MonoBehaviour {

	public Text text;
	public GameObject thinking;
	public GameObject speaking;

	// Use this for initialization
	void Start () {
		text.text = "";
		thinking.SetActive(false);
		speaking.SetActive(false);
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D col) {
		StartCoroutine ("DialogueOptions");
	}

	public IEnumerator DialogueOptions () {
		Debug.Log ("we started the options");
		thinking.SetActive(true);

		while (text.text != "Option one" && text.text != ("Option two")) {
			yield return new WaitForSeconds (.01f);
			if (Input.GetKeyDown ("1")) {
				thinking.SetActive(false);
				text.text = "Option one";
				Debug.Log ("Option one selected");
			} else if (Input.GetKeyDown ("2")) {
				thinking.SetActive(false);
				text.text = "Option two";
				Debug.Log ("Option two selected");
			}
		}
		speaking.SetActive(true);
		text.enabled = true;
		yield return new WaitForSeconds (2.0f);
		speaking.SetActive(false);
		text.enabled = false;
	}
}
