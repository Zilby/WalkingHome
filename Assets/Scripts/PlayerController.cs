using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public GameObject friend; // the friend
    protected Rigidbody2D hero; // the hero's physics body
	public SpriteRenderer sr; // the hero's sprite renderer
	public List<GameObject> paranoi;
	public List<GameObject> frustrates;
	protected bool f1 = false; 
	protected bool f2 = false;
	// public SpriteRenderer friendRend; // the friend's sprite renderer
	protected bool characterPause; // determines if the character will be stopped
	protected Sprite orig;
	protected Sprite friendOrig;
	protected bool paranoid = false;
	protected bool frustrate = false;

    public float moveSpeed; // determines the moveSpeed of the hero
	public List<GameObject> initialThoughts;



	protected int counter = 0; // used to modify meters

	protected Animator anim; // controls the animations for this hero
	protected Animator friendAnim; // controls the animations for the friend

    //-Paranoia/anxiety: If paranoia/anxiety reaches its max capacity, speed increases? amount of blobs increases?
    //-Confidence:       If confidence reaches its minimum capacity, the player’s paranoia/anxiety and frustration meters
    //                   are impacted significantly more by attacks of harassment until the player can regain their
    //                   confidence.
    //-Frustration:      

    // Use this for initialization
    void Start () {
        hero = GetComponent<Rigidbody2D> (); // gets the physics body of the hero
		anim = GetComponent<Animator> (); // gets the animator for the hero
		if (friend != null) {
			friendAnim = friend.GetComponent<Animator> ();
		}
		sr = GetComponent<SpriteRenderer> (); // gets the sprite renderer for the hero

		characterPause = false; // initializes character to not be stopped
		orig = sr.sprite;
		//if (friendRend != null) {
		//	friendOrig = friendRend.sprite;
		//}
		if (SceneManager.GetActiveScene().name.Equals("EndScroller")) {
			hero.constraints = RigidbodyConstraints2D.None;
			hero.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
		StartCoroutine (InitialThoughts ());
	}
	
	// Update is called once per frame
	void Update () {
        float xDir = Input.GetAxis("Horizontal"); // reads horizontal inputs from the keyboard
        float yDir = Input.GetAxis("Vertical"); // reads vertical inputs from the keyboard

		if (characterPause) {
			characterPauseMove (); // pauses the hero's movement 
		}
		else {
			MovePlayer (xDir, yDir); // moves the character
		}
		if (GameController.paranoia > 50 && !paranoid) {
			StartCoroutine (Paranoid ());
			paranoid = true;
		}
		if ((GameController.frustration == 30 || GameController.frustration == 70) && !frustrate) {
			if (GameController.frustration == 30 && !f1) {
				f1 = true;
				StartCoroutine (Frustrated ());
				frustrate = true;
			} else if (GameController.frustration == 70 && !f2) {
				f2 = true;
				StartCoroutine (Frustrated ());
				frustrate = true;
			}
		}
	}

	// moves the player, given two input floats
    private void MovePlayer(float horizontal, float vertical) {
		//determine sprite flipping
		if (horizontal < 0) {
			// face left
			sr.flipX = true;
			if (friend) {
				if (!friend.GetComponent<Friend> ().independent) {
					friend.GetComponent<SpriteRenderer> ().flipX = true;
				}
			}
		}
		if (horizontal > 0) {
			// face right
			sr.flipX = false;
			if (friend) {
				if (!friend.GetComponent<Friend> ().independent) {
					friend.GetComponent<SpriteRenderer> ().flipX = false;
				}
			}
		}

		// determine animation displayed TODO vertical animations?
		if (Mathf.Abs (horizontal) >= 0.1) {
			// walking animation for horizontal direction
			anim.enabled = true;
			if (friendAnim != null) {
				friendAnim.enabled = true;
			}
		} else {
			// idle animation
			stopAnimation();
			sr.sprite = orig;
			//if (!PlayerChoice.fixAnim) {
			//	friendRend.sprite = friendOrig;
			//}
		}

		// actual movement for the hero occurs here by applying a change to the hero's velocity
        hero.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }

	// causes the hero to stop moving
	protected void characterPauseMove () {
		hero.velocity = new Vector2 (0, 0);
	}

	// stops the hero's movement animation
	public void stopAnimation() {
		StartCoroutine(PauseAnim());
	}

	protected IEnumerator PauseAnim() {
		yield return new WaitForSeconds (0.00000001f);
		while (sr.sprite != orig) {
			yield return new WaitForSeconds (0.00000001f);
		}
		anim.enabled = false;
		if (friendAnim != null && !PlayerChoice.fixAnim) {
			friendAnim.enabled = false;
		}
	}

	public IEnumerator InitialThoughts() {
		yield return new WaitForSeconds (1f);
		foreach (GameObject g in initialThoughts) {
			g.SetActive (true);
			yield return new WaitForSeconds (3.5f);
			g.SetActive (false);
		}
	}

	public IEnumerator Paranoid() {
		yield return new WaitForSeconds ((130 - GameController.paranoia) / 2);
		int i = Random.Range (0, paranoi.Count);
		characterPause = true;
		paranoi[i].SetActive (true);
		sr.flipX = !sr.flipX;
		StartCoroutine(PauseAnim());
		yield return new WaitForSeconds (3f);
		characterPause = false;
		paranoi[i].SetActive (false);
		paranoid = false;
	}

	public IEnumerator Frustrated() {
		yield return new WaitForSeconds (9);
		int i = Random.Range (0, frustrates.Count);
		characterPause = true;
		frustrates[i].SetActive (true);
		StartCoroutine(PauseAnim());
		yield return new WaitForSeconds (3f);
		characterPause = false;
		frustrates[i].SetActive (false);
		frustrate = false;
	}

	public bool CharacterPause
	{
		get { return characterPause; }
		set { characterPause = value; }
	}
}
