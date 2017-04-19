using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollSceneChangeController : MonoBehaviour {

	public Collider2D heroColl; // the hero's collider
	private Collider2D sceneColl; // the scene changing collider

	// Use this for initialization
	void Start () {
		heroColl = GameObject.FindGameObjectWithTag ("hero").GetComponent<Collider2D> (); // gets the hero's collider
		sceneColl = GetComponent<Collider2D> (); // gets the scene changing collider
	}
	
	// Update is called once per frame
	void Update () {
		MakeScene (); // calls the scene switching function
	}

	// switches the scene on collision between the scene switcher box collider and the hero
	void MakeScene() {
		if (heroColl.IsTouching (sceneColl)) {
			GameController.frustration -= 2;
			if (SceneManager.GetActiveScene ().name.Equals ("Scroller")) {
				SceneManager.LoadSceneAsync ("CityTransition"); // loads the next level
			} else if (SceneManager.GetActiveScene ().name.Equals ("EndScroller")) {
				SceneManager.LoadSceneAsync ("CreditsTransition");
			}
		}
	}
}
