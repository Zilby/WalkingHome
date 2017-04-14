using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour {

    public GameObject hero; // the hero GameObject
    public GameObject enemy; // the enemy GameObject

    public Rigidbody2D enemyBody; // the enemy's physics body

    public bool enemyTouchingWall; // determines if the enemy is touching a wall
	public static bool enemiesMove; // determines if enemies can move

	// public Transform[] waypoints; // array of points for the enemy to move between
	// private int cur = 0;

	[SerializeField]
	private float moveSpeed; // represents the movespeed of the enemy

    private Collider2D heroColl; // the hero's collider
    private Collider2D enemyColl; // the enemy's collider

    // Use this for initialization
    void Start () {

        hero = GameObject.FindGameObjectWithTag("hero"); // gets the hero GameObject TODO just give it the hero in the inspector
        enemy = GameObject.FindGameObjectWithTag("enemy"); // gets the enemy GameObject TODO just give it the enemy in the inspector

        enemyTouchingWall = false; // initally the enemy is not touching a wall
		enemiesMove = false;

        heroColl = hero.GetComponent<BoxCollider2D>(); // gets the hero's collider
        //enemyColl = enemy.GetComponent<BoxCollider2D>(); // gets the enemy's collider
    }

    // Update is called once per frame
    void FixedUpdate () {
		// StalkPlayer (); // the movement function for the enemy that chases the hero blindly TODO introduce wall checking
		// WaypointMove (); // the movement function for an enemy that moves between a series of waypoints TODO
    }

	// a movement function that blindly applies forces that direct the enemy towards the hero
    void StalkPlayer ()
    {
        Vector2 pos = Vector2.Lerp((Vector2)transform.position, (Vector2)hero.transform.position, Time.fixedDeltaTime);
		transform.position = new Vector3(pos.x, pos.y, transform.position.z); // TODO was transform.position.y ... should be .z?
    }

	public static void EnemiesMove() {
		enemiesMove = true;
	}

	void WaypointMove() {
		/*
		if (transform.position != waypoints[cur].position) {
			Vector2 p = Vector2.MoveTowards(transform.position,
				waypoints[cur].position,
				moveSpeed);
			enemyBody.MovePosition(p);
		}
		// Waypoint reached, select next one
		else cur = (cur + 1) % waypoints.Length;
		*/
	}

	// this function uses Linecasting to determine where the enemy is able to go to
	void MoveEnemy () {
		Vector2 startPos = enemy.transform.position;
		Vector2 towardsHeroPos = Vector2.Lerp( (Vector2) transform.position, (Vector2) hero.transform.position, Time.fixedDeltaTime);
		Vector2 towardsHeroPosX = new Vector2 (towardsHeroPos.x, startPos.y);
		Vector2 towardsHeroPosY = new Vector2 (startPos.x, towardsHeroPos.y);
		Vector2 towardsHeroNegX = new Vector2 (-1 * towardsHeroPosX.x, startPos.y); // need more effective negative checker
		Vector2 towardsHeroNegY = new Vector2 (startPos.x, -1 * towardsHeroPosY.y); // need more effective negative checker

		if (Physics2D.Linecast (startPos, towardsHeroPos) == null) {
			StalkPlayer ();
		} else if (Physics2D.Linecast (startPos, towardsHeroPosX)) {
			Vector2 goTo = Vector2.Lerp (startPos, towardsHeroPosX, Time.fixedDeltaTime);
			transform.position = new Vector3 (goTo.x, goTo.y, transform.position.z); // TODO was transform.position.y ... should be .z?
		} else if (Physics2D.Linecast (startPos, towardsHeroPosY)) {
			Vector2 goTo = Vector2.Lerp (startPos, towardsHeroPosY, Time.fixedDeltaTime);
			transform.position = new Vector3 (goTo.x, goTo.y, transform.position.z); // TODO was transform.position.y ... should be .z?
		} 
		/*
		 * This will have more branches of conditions to try to go in other directions when the linecasts fail
		 * need to have a normalized movement behavior for the AI to be intelligent and not AD (artifially dumb)
		 * pl0x h3lp m3 w!th th!$ thX
		 * 
		 */
	}

}