using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script that rotates an object on 0-3 axes at once at individual speeds
 */

public class ObjectSpinner : MonoBehaviour {

	//speeds of rotation
	public float xSpeed;
	public float ySpeed;
	public float zSpeed;

	//rotate the object in the three axes in terms of global space
	void Update() {
		transform.Rotate (Vector3.right * xSpeed * Time.deltaTime, Space.World);
		transform.Rotate (Vector3.up * ySpeed * Time.deltaTime, Space.World);
		transform.Rotate (Vector3.forward * zSpeed * Time.deltaTime, Space.World);
	}
}
