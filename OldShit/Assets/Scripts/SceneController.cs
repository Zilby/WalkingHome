using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    private Collider2D heroCollide; // the hero's collider

    // Use this for initialization
    void Start () {
        heroCollide = GetComponent<Collider2D>(); // gets the hero's collider
    }
	
	// Update is called once per frame
	void Update () {
        OnTriggerEnter2D(heroCollide); // checks to see if the hero's collider has collided with this collider
    }

	// overrides OnTriggerEnter2D(Collider2D co) to have a behavior that loads a different scene
    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "hero0") // checks to see if the Collider2D that collided with this script's collider is named hero0
        {
            Destroy(co.gameObject); // destroys the hero that collided with this scene change
			SceneManager.UnloadSceneAsync("WalkingHome");
            SceneManager.LoadScene("FeMazinist"); // loads the next scene
        }
    }
}
