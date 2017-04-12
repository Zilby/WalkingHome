using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// an older script that controls the speech of the hero in the original maze TODO
public class SpeechController : MonoBehaviour {

    public Renderer rend;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        ToggleHarass();
    }

    void ToggleHarass ()
    {
		if (Time.timeSinceLevelLoad % 2 > 1.0f) {
        	rend.enabled = true;
        }
		else if (Time.timeSinceLevelLoad % 2 < 1.0f) {
        	rend.enabled = false;
        }
    }
}
