using UnityEngine;
using System.Collections;

/*
 * Script that destroys any objects that leave the surrounding boundary box
 */

public class DestroyByBoundary : MonoBehaviour {

	//only called when an object _leaves_ the collider of the boundary
	void OnTriggerExit(Collider other) {
		Destroy (other.gameObject);
	}
}
