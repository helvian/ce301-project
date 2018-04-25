using UnityEngine;
using System.Collections;

/*
 * Script used by side-flying enemies to know when to shoot their weapons 
 * The "scan box" is a large collider that extends vertically above and below the enemy
 */

public class ScanBox : MonoBehaviour {

	public SideEnemyController sec; //associated script

	//initialisation
	void Start() {
		sec = GetComponentInParent<SideEnemyController> ();
	}

	//when the player ship enters the scan box
	void OnTriggerEnter(Collider other) {
		//ignore the lockon collider
		if (other.tag == "Lockon") {
			return;
		}
		//fire the weapons
		sec.Fire ();
	}


}
