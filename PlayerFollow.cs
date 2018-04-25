using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script that controls how an object should follow the player (in this case, the Options)
 * This script is attached to an Option
 */

public class PlayerFollow : MonoBehaviour {

	public GameObject player; //the player
	public PowerupController pc; //associated script to keep track of number of options spawned
	public int followDistance; //how far from the player to stay at
	private List<Vector3> recordedPositions; //positions of the player at a point in time
	//each position represents 1 frame of movement

	//initialisations
	void Awake() {
		recordedPositions = new List<Vector3> ();
	}
	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
		pc = GameObject.FindGameObjectWithTag ("Powerup Controller").GetComponent<PowerupController> ();
		followDistance = pc.optionsSpawned * 15;
	}
		

	void Update () {
		//first recorded movement
		if (recordedPositions.Count == 0) {
			recordedPositions.Add (player.transform.position);
			return;
		} 
		//if the player has moved since last recorded frame
		else if (recordedPositions[recordedPositions.Count-1] != player.transform.position) {
			recordedPositions.Add (player.transform.position);
		}
		//if there have been enough frames recorded
		if (recordedPositions.Count > followDistance) {
			//move this Option to the first position then delete it
			transform.position = recordedPositions [0];
			recordedPositions.RemoveAt (0);
		}
	}
}
