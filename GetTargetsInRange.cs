using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script used for homing weapon
 * Finds all enemies inside a circle around the player and marks them for one homing attack
 */

public class GetTargetsInRange : MonoBehaviour {

	public List <Collider> targetsInRange = new List <Collider>(); //the list of enemies

	//main function
	public void ScanInSphere() {
		targetsInRange.Clear (); //clear the list every iteration 
		Collider[] inRange = new Collider[0]; //set up a new array of all colliders in range
		inRange = Physics.OverlapSphere (gameObject.transform.position, 20); //search in a 20 unit sphere around the player

		//add a mark to every enemy found in range
		for (int i = 0; i < inRange.Length; i++) { 
			if (inRange [i].tag == "Enemy" || inRange[i].tag == "Boss") {
				targetsInRange.Add (inRange [i]);
			}
		}
	}
		
}
