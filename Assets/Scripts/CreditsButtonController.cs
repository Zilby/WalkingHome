using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsButtonController : MonoBehaviour {

	public Button creditsButton; // the credits button

	// Use this for initialization
	void Start () {
		creditsButton = GetComponent<Button> (); // gets the credits button
		creditsButton.onClick.AddListener(CREDITS); // sets an onclick function for the credits button
	}

	// Update is called once per frame
	void Update () {

	}

	// roll the credits boys and girls
	void CREDITS() {
		SceneManager.LoadScene("Credits"); // loads the Credits scene
	}
}
