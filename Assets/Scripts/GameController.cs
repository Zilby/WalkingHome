﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static int paranoia; // synonymous with anxiety in discussion below
	public static int confidence; // see def below
	public static int frustration; // see def below
	public Slider fSlider;
	public Slider pSlider;
	public Slider cSlider;
	public GameObject stats;
	public GameObject timer;

	// public PlayerController p; TODO

	private int secondsTilTrain = 480;
	private int counter;

	public void Start() {
		paranoia = 0; // initializes paranoia to min
		confidence = 100; // initializes confidence to max
		frustration = 0; // initializes frustration to min
		counter = secondsTilTrain;
		StartCoroutine(Doomsday());
	}

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Update() {
		string s = SceneManager.GetActiveScene ().name;
		if (s == "Scroller" || s == "Scroller3" || s == "Maze" || s == "City" || s == "Maze2" || s == "City2" || s == "EndScroller") {
			stats.SetActive (true);
			timer.SetActive (true);
		} else {
			stats.SetActive (false);
			timer.SetActive (false);
		}

		frustration = Mathf.Clamp (frustration, 0, 100);
		paranoia = Mathf.Clamp (paranoia, 0, 100);
		confidence = Mathf.Clamp (confidence, 0, 100);

		fSlider.value = frustration;
		pSlider.value = paranoia;
		cSlider.value = confidence;

		// update the player's move speed based on the current stats of the player
		// CONSIDERATIONS:
		// -- Confidence starts at 100
		// -- Frustration/Paranoia start at 0
		// -- Each stat can have a slowing/speeding effect on the player
		// p.moveSpeed = p.moveSpeed * (cSlider.value / (pSlider.value + fSlider.value)); // TOTALLY RANDOM MATH TODO
	}

	public IEnumerator Doomsday() {
		while (counter > 0) {
			if (SceneManager.GetActiveScene ().name != "TitleScreen" && SceneManager.GetActiveScene ().name != "OpenerText") {
				counter--;
			}

			timer.GetComponent<Text> ().text = "Time Until Train: " + ClockTime (counter);
			yield return new WaitForSeconds (1.0f);
		}
		SceneManager.LoadScene ("Failure");
	}

	string ClockTime(int sec) {
		int seconds = sec % 60;
		int minutes = sec / 60;

		string minStr = minutes.ToString ();
		string secStr;
		if (seconds < 10) {
			secStr = "0" + seconds.ToString ();
		} else {
			secStr = seconds.ToString ();
		}

		return minStr + ":" + secStr;
	}
}
