using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour {

	public GameObject player;
	public PowerupController pc;
	public int followDistance;
	private List<Vector3> recordedPositions;

	void Awake() {
		recordedPositions = new List<Vector3> ();
	}

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
		pc = GameObject.FindGameObjectWithTag ("Powerup Controller").GetComponent<PowerupController> ();
		followDistance = pc.optionsSpawned * 15;
	}
		
	void Update () {
		if (recordedPositions.Count == 0) {
			Debug.Log ("first movement");
			recordedPositions.Add (player.transform.position);
			return;
		} 
		else if (recordedPositions[recordedPositions.Count-1] != player.transform.position) {
			Debug.Log ("movement");
			recordedPositions.Add (player.transform.position);
		}
		if (recordedPositions.Count > followDistance) {
			transform.position = recordedPositions [0];
			recordedPositions.RemoveAt (0);
		}
	}
}
