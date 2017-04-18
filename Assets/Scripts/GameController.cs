using System.Collections;
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

	public void Start() {
		paranoia = 0; // initializes paranoia to min
		confidence = 100; // initializes confidence to max
		frustration = 0; // initializes frustration to min
	}

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Update() {
		string s = SceneManager.GetActiveScene ().name;
		if (s == "Scroller" || s == "Scroller3" || s == "Maze" || s == "City" || s == "Maze2" || s == "EndScroller") {
			stats.SetActive (true);
		} else {
			stats.SetActive (false);
		}

		fSlider.value = frustration;
		pSlider.value = paranoia;
		cSlider.value = confidence;
	}
}
