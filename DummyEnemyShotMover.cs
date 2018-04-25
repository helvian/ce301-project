using UnityEngine;
using System.Collections;

/*
 * Legacy script used to move shots before the polarity system was created
 */

public class DummyEnemyShotMover : MonoBehaviour {
	private Rigidbody rb;
	public float speed;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;
	}
}
