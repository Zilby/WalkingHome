using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParkExit : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "hero") {
			SceneManager.LoadScene ("MazeTransition");
		}
	}
}
