using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTargetsInRange : MonoBehaviour {

	public List <Collider> targetsInRange = new List <Collider>();

	public void ScanInSphere() {
		Debug.Log ("scan");
		targetsInRange.Clear ();
		Collider[] inRange = new Collider[0];
		inRange = Physics.OverlapSphere (gameObject.transform.position, 20);
		for (int i = 0; i < inRange.Length; i++) {
			if (inRange [i].tag == "Enemy" || inRange[i].tag == "Boss") {
				targetsInRange.Add (inRange [i]);
			}
		}
	}

	/*void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy" || other.tag == "Boss") {
			targetsInRange.Add (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Enemy" || other.tag == "Boss") {
			targetsInRange.Remove (other.gameObject);
		}
	}*/
}
