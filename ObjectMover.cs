using UnityEngine;
using System.Collections;

/*
 * Another mover script similar to DummyEnemyShotMover, kept separate for easier expansion
 */
  
public class ObjectMover : MonoBehaviour {
	
	private Rigidbody rb; //the physics body of the object that has this script 
	public float speed; //how fast to move the object

	//keep moving the object in the same direction at the same speed
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = Vector3.forward * speed;
	}
}
