using UnityEngine;
using System.Collections;

public class MissileSeek : MonoBehaviour {

	public GameObject target;
	public Rigidbody rb;

	public Vector3 initialVelocity;
	public Vector3 currentVelocity;
	public Vector3 desiredVelocity;
	public Vector3 desiredDirection;
	public float speed;

	public EnemyStats es;
	public TextController tc;
	private EffectOnDeath eod;

	private bool dead = false;

	void Start () {
		eod = GetComponent<EffectOnDeath> ();
		es = GetComponent<EnemyStats> ();
		target = GameObject.FindGameObjectWithTag ("Player");
		rb = GetComponent<Rigidbody> ();
		tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();

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
		seek ();

		currentVelocity += (desiredDirection / rb.mass);
		currentVelocity.Normalize ();
		currentVelocity *= speed;

		rb.velocity = currentVelocity;

		transform.rotation = Quaternion.LookRotation (currentVelocity);
	}

	public void TakeDamage(float damage) {
		es.health -= damage;
		if (es.health <= 0) {
			if (!dead) {
				Death ();
			}
		}
	}

	void Death() {
		eod.SpawnEffect ();
		dead = true;
		tc.UpdateScore (es.score);
		Destroy (gameObject);
	}
}
