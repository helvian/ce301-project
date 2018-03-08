using UnityEngine;
using System.Collections;

public class HomingAttack : MonoBehaviour {

	public GameObject target;
	public Rigidbody rb;

	public Vector3 initialVelocity;
	public Vector3 currentVelocity;
	public Vector3 desiredVelocity;
	public Vector3 desiredDirection;
	public float speed;

	//public EnemyStats es;
	//public TextController tc;

	void Start () {
		//es = GetComponent<EnemyStats> ();
		rb = GetComponent<Rigidbody> ();
		//tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();

		rb.velocity = initialVelocity;
		currentVelocity = rb.velocity;
	}

	void seek() {
		desiredVelocity = target.transform.position - rb.transform.position;
		desiredVelocity.Normalize ();
		desiredVelocity *= speed;

		desiredDirection = desiredVelocity - currentVelocity;
	}

	void FixedUpdate () {
		if (target == null) {
			Destroy (gameObject);
		} else {
			seek ();

			currentVelocity += (desiredDirection / rb.mass);
			currentVelocity.Normalize ();
			currentVelocity *= speed;

			rb.velocity = currentVelocity;
			rb.mass = rb.mass * 0.92f;
			speed *= 1.1f;

			transform.rotation = Quaternion.LookRotation (currentVelocity);
		}
	}

	/*public void TakeDamage(float damage) {
		es.health -= damage;
		if (es.health <= 0) {
			tc.UpdateScore (es.score);
			Destroy (gameObject);
		}
	}*/
}
