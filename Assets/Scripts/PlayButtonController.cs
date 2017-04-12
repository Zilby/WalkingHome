using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonController : MonoBehaviour {

	public Button playButton; // the play button

	// Use this for initialization
	void Start () {
		playButton = GetComponent<Button> (); // get the play button
		playButton.onClick.AddListener(PLAY); // add an onclick function to the play button
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// plays the game!
	void PLAY() {
		SceneManager.LoadScene("OpenerText");
	}
}
