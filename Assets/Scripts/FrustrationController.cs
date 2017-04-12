using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrustrationController : MonoBehaviour {

    public GameObject hero; // the hero's GameObject
    public GameObject enemy; // an enemy's GameObject

    public Slider slider; // the slider for frustration
	public Image frustrationFill; // the fill inside the slider's image

    public int counter; // a counter to help with changing values based on ticks

	// Use this for initialization
	void Start () {
		hero = GameObject.FindGameObjectWithTag("hero"); // gets the hero GameObject
        enemy = GameObject.FindGameObjectWithTag("enemy"); // gets the enemy GameObject

        // slider = GameObject.FindGameObjectWithTag("frustration_meter").GetComponent<Slider>(); TODO
        slider.value = 0; // the slider's value is initialized to 0

		// TODO get the fill image
        counter = 0; // initializes the counter to start from 0
    }

    // Update is called once per frame
    void Update () {

		// this if-else is supposed to determine whether or not the fill image will be rendered TODO 
		/*if (slider.value == 0) {
			frustrationFill.GetComponent<Image> ().fillAmount = 0;
		} else {
			frustrationFill.GetComponent<Image> ().fillAmount = 1;
		}*/

		// this increments the counter on-tick to help determine if the frustration value on the slider should be incremented
		if (closeTo(hero, enemy))
        {
            counter += 1; // counter is incremented once per frame that the if condition is satisfied
        }

		// if the counter is equal to or exceeds 150, then several updates occur
        if (counter >= 150)
        {
            slider.value += 1; // increments the value of the frustration slider
			// hero.GetComponent<PlayerController> ().frustration += 1; // increments the value of the hero's frustration... TODO static implementation
            counter = 0; // resets the counter
        }
	}

	// determines if the hero is close to a given enemy
    public static bool closeTo (GameObject h, GameObject e)
    {
        Vector3 heroPosn; // the hero's position
		Vector3 enemyPosn; // the enemy's position

		// if the hero doesn't exist
        if (h == null)
        {
			return false; // it can't be close to the enemy
        } else {
			heroPosn = h.transform.position; // otherwise get the hero's position and determine if it is
        }

		// if the enemy doesn't exist
		if (e == null) {
			return false; // it can't be close to the hero
		} else {
			enemyPosn = e.transform.position; // otherwise get the enemy's position and determine if it is
		}
			
        float xDiff = heroPosn.x - enemyPosn.x; // determine the diference between the x positions
        float yDiff = heroPosn.y - enemyPosn.y; // determine the difference between the y positions
        float xDSquared = Mathf.Pow(xDiff, 2); // square the difference
        float yDSquared = Mathf.Pow(yDiff, 2); // square the difference
        float sumSquares = xDSquared + yDSquared; // sum the differences

		// finish calculating the Euclidean(?) disance
		// between the hero and the enemy and determine 
		// if they are "close" to one another (within five units)
		return Mathf.Sqrt(sumSquares) <= 5.0; 
    }
}
