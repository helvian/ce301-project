using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Function that ferries an enemy to a given point in the screen
 */

public class MoveToWaypoint : MonoBehaviour {

	//locations that enemies can spawn and move to
	public Transform[] possibleWaypoints = new Transform[2];
	public Transform[] possibleSpawns = new Transform[2];

	//where the enemy will spawn
	public int position;

	//how fast the enemy will move towards the point
	public int speed;
	public float step;

	//initialisation
	void Start() {
		possibleWaypoints [0] = GameObject.Find ("Top Left Waypoint").transform;
		possibleWaypoints [1] = GameObject.Find ("Top Right Waypoint").transform;
		possibleSpawns [0] = GameObject.Find ("Spawn Top Left").transform;
		possibleSpawns [1] = GameObject.Find ("Spawn Top Right").transform;

		//choose a position and spawn the enemy there
		position = Random.Range (0, 2);
		if (position == 0) {
			gameObject.transform.position = possibleSpawns [0].position;
		} else {
			gameObject.transform.position = possibleSpawns [1].position;
		}
		step = speed * Time.deltaTime;
	}

	//move the enemy towards their given waypoint increasingly slower over time
	void FixedUpdate() {
		if (position == 0) {
			transform.position = Vector3.MoveTowards (transform.position, possibleWaypoints [0].position, step * Time.deltaTime);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, possibleWaypoints [1].position, step * Time.deltaTime);
		}
		step *= 0.995f;
	}
}
