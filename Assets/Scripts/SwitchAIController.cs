using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAIController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("hey just passin' through");
		EnemyController.EnemiesMove ();
	}
}
