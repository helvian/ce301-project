using UnityEngine;
using System.Collections;

/*
 * This and DummyEnemyShotMover are separate for now in case I want them to do different things later
 */
  
public class ObjectMover : MonoBehaviour {
	private Rigidbody rb;
	public float speed;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = Vector3.forward * speed;
	}
}
