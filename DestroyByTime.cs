using UnityEngine;
using System.Collections;

/*
 * Simple cleaner script that destroys any object that has this script on it after 
 * a given number of seconds
 */

public class DestroyByTime : MonoBehaviour {
	
	public float lifetime; //time to wait before destroying

	void Start () {
		//Destroy(GameObject, float) - destroy the GameObject after float seconds
		Destroy(gameObject, lifetime);
	}

}
