using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// outdated script to control visibility of pieces of the maze via cloud sprites that would cover various areas. TODO
public class VisibilityController : MonoBehaviour {

	GameObject cloud;
	GameObject hero;
	Collider2D heroColl;

	[SerializeField]
	private Collider2D cloudColl;

	// Use this for initialization
	void Start () {
		cloud = this.gameObject;
		hero = GameObject.FindGameObjectWithTag ("hero");
		heroColl = hero.GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (cloudColl.IsTouching (heroColl)) {
			Destroy (cloud);
		}
	}
}
