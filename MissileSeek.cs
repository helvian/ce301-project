using UnityEngine;
using System.Collections;

/*
 * Function that controls the movement of the homing missile enemies
 */

public class MissileSeek : MonoBehaviour {

	public GameObject target; //the destination of the missile
	public Rigidbody rb; //the physics body of the missile

	//vectors involved in the seek steering behaviour
	public Vector3 initialVelocity;
	public Vector3 currentVelocity;
	public Vector3 desiredVelocity;
	public Vector3 desiredDirection;

	public float speed; //how fast the projectile travels

	//associated scripts
	public EnemyStats es;
	public TextController tc;
	private EffectOnDeath eod;

	private bool dead = false; //if the missile is dead

	//initialisation
	void Start () {
		eod = GetComponent<EffectOnDeath> ();
		es = GetComponent<EnemyStats> ();
		target = GameObject.FindGameObjectWithTag ("Player");
		rb = GetComponent<Rigidbody> ();
		tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();

		rb.velocity = initialVelocity;
		currentVelocity = rb.velocity;
	}

	//calculate the direction the projectile needs to go to make contact
	void seek() {
		//calculate where the target is in relation to this projectile
		//make this vector longer according to the speed
		desiredVelocity = target.transform.position - rb.transform.position;
		desiredVelocity.Normalize ();
		desiredVelocity *= speed;

		//the direction is where the projectile needs to go versus where it is currently going
		desiredDirection = desiredVelocity - currentVelocity;
	}

	void FixedUpdate () {
		seek ();

		//add the desired direction to the velocity taking mass into account
		currentVelocity += (desiredDirection / rb.mass);
		currentVelocity.Normalize ();
		currentVelocity *= speed;

		rb.velocity = currentVelocity;

		//face the direction of travel
		transform.rotation = Quaternion.LookRotation (currentVelocity);
	}

	//called when the player shoots the missile
	public void TakeDamage(float damage) {
		es.health -= damage;
		if (es.health <= 0) {
			if (!dead) {
				Death ();
			}
		}
	}

	//called when the missile reaches 0 health
	void Death() {
		//spawn an explosion, update score, delete missile
		eod.SpawnEffect ();
		dead = true;
		tc.UpdateScore (es.score);
		Destroy (gameObject);
	}
}
