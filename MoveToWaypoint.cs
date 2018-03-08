using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToWaypoint : MonoBehaviour {

	public Transform[] possibleWaypoints = new Transform[2];
	public Transform[] possibleSpawns = new Transform[2];
	public int position;
	public int speed;
	public float step;

	void Start() {
		possibleWaypoints [0] = GameObject.Find ("Top Left Waypoint").transform;
		possibleWaypoints [1] = GameObject.Find ("Top Right Waypoint").transform;
		possibleSpawns [0] = GameObject.Find ("Spawn Top Left").transform;
		possibleSpawns [1] = GameObject.Find ("Spawn Top Right").transform;
		position = Random.Range (0, 1);
		if (position == 0) {
			gameObject.transform.position = possibleSpawns [0].position;
		} else {
			gameObject.transform.position = possibleSpawns [1].position;
		}
		step = speed * Time.deltaTime;
	}

	void FixedUpdate() {
		if (position == 0) {
			Debug.Log ("lerp");
			Debug.Log (possibleWaypoints [0].position);
			transform.position = Vector3.MoveTowards (transform.position, possibleWaypoints [0].position, step * Time.deltaTime);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, possibleWaypoints [1].position, 1f * Time.deltaTime);
		}
		step *= 0.995f;
	}
}
