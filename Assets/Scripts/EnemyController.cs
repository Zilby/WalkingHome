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

	public Transform[] waypoints; // array of points for the enemy to move between
	private int cur = 0;

	[SerializeField]
	private float moveSpeed; // reresents the movespeed of the enemy

    private Collider2D heroColl; // the hero's collider
    private Collider2D enemyColl; // the enemy's collider
	private Collider2D[] walls; // array of colliders representing the walls of the map

    private float Range; // a range to represent something or other TODO lol

	private Ray2D wallChecker;

    // Use this for initialization
    void Start () {

        hero = GameObject.FindGameObjectWithTag("hero"); // gets the hero GameObject
        enemy = GameObject.FindGameObjectWithTag("enemy"); // gets the enemy GameObject

        // enemyBody = enemy.GetComponent<Rigidbody2D>(); // gets the enemy's physics body

        enemyTouchingWall = false; // initally the enemy is not touching a wall
		enemiesMove = false;

        heroColl = hero.GetComponent<BoxCollider2D>(); // gets the hero's collider
        //enemyColl = enemy.GetComponent<BoxCollider2D>(); // gets the enemy's collider
    }

    // Update is called once per frame
    void FixedUpdate () {
		// StalkPlayer (); // the movement function for the enemy that chases the hero blindly TODO 
		WaypointMove ();
    }

	// a movement function to get the enemy unstuck and to help in its futile chasing of the hero
    void RandomMoveEnemy ()
    {
        // introduce randomness
        float pickOne = Random.value;

        if (pickOne <= 0.25)
        {
            // go up
            enemyBody.velocity = new Vector2(0, -1 * pickOne * moveSpeed);
        }
        else if (pickOne <= 0.50)
        {
            // go left
            enemyBody.velocity = new Vector2(-1 * pickOne * moveSpeed, 0);
        }
        else if (pickOne <= .75)
        {
            // go right
            enemyBody.velocity = new Vector2(pickOne * moveSpeed, 0);
        }
        else
        {
            // go down
            enemyBody.velocity = new Vector2(0, pickOne * moveSpeed);
        }
    }

	// a movement function that blindly applies forces that direct the enemy towards the hero
    void StalkPlayer ()
    {
        Vector2 pos = Vector2.Lerp((Vector2)transform.position, (Vector2)hero.transform.position, Time.fixedDeltaTime);
        transform.position = new Vector3(pos.x, pos.y, transform.position.y);
    }

	/*void WallCheck () {
		foreach (Collider2D c in walls) {
			if (RaycastHit2D.Equals (c)) {
				// theres a wall in the way,
				// so move in a different way!
			}
		}
	}*/

	public static void EnemiesMove() {
		enemiesMove = true;
	}

	void WaypointMove() {
		if (transform.position != waypoints[cur].position) {
			Vector2 p = Vector2.MoveTowards(transform.position,
				waypoints[cur].position,
				moveSpeed);
			enemyBody.MovePosition(p);
		}
		// Waypoint reached, select next one
		else cur = (cur + 1) % waypoints.Length;
	}
}

