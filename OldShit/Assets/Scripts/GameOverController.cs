using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r")) // if r is pressed
        {
            SceneManager.LoadScene("Scroller"); // restarts the game by reloading the first level
        }
    }
}
