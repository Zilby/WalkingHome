using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public void Start() {
		Time.timeScale = 1.0f; 
	}

	// plays the game!
	public void Play() {
		SceneManager.LoadScene("OpenerText");
	}

	// roll the credits boys and girls
	public void Credits() {
		SceneManager.LoadScene("Credits"); // loads the Credits scene
	}
}
