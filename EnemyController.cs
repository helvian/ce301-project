using UnityEngine;
using System.Collections;

/*
 * Brain script for forward-flying enemies
 */

public class EnemyController : MonoBehaviour {
	private Rigidbody rb; //the physics body attached

	public ParticleSystem shot; // the weapon attached

	public GameObject spray; //the effect played when attacked

	public GameObject powerUp; //the powerup to spawn if dropsPowerup is enabled
	public bool dropsPowerUp; //if the enemy should drop a powerup on death
	private bool dead = false; //if the ship is dead

	//associated scripts
	private EnemyStats es;
	private EffectOnDeath eod;
	public TextController tc;

	//initialise scripts into variables
	void Start () {
		es = GetComponent<EnemyStats> ();
		eod = GetComponent<EffectOnDeath> ();
		rb = GetComponent<Rigidbody> ();
		shot = GetComponentInChildren<ParticleSystem> ();
		tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();

		//set velocity based on inputted speed
		rb.velocity = transform.forward * es.speed;
	}

	//called when the ship is attacked
	public void TakeDamage(float damage) {
		es.health -= damage;
		Instantiate (spray, transform.position, Quaternion.identity);
		if (es.health <= 0) {
			
			//can only be called once (because two of these functions can be called at once due to
			//how the bullets are fired and make contact)
			if (!dead) {
				Death ();
			}
		}
	}

	//played when ship reaches zero health
	void Death() {
		eod.SpawnEffect ();
		dead = true;

		//drop powerup if this enemy is flagged as carrying a powerup
		if (dropsPowerUp) {
			Instantiate (powerUp, transform.position, Quaternion.Euler(90, 0, 0));
		}
		tc.UpdateScore (es.score);
		shot.Stop ();

		//re-parenting, re-sizing, and timing the destruction of the weapons attached
		Vector3 tempVec = shot.transform.lossyScale;
		Debug.Log (shot.transform.lossyScale);
		tempVec.x = 1 / shot.transform.parent.lossyScale.x;
		tempVec.y = 1 /shot.transform.parent.lossyScale.y;
		tempVec.z = 1 /shot.transform.parent.lossyScale.z;
		shot.transform.localScale = tempVec;

		shot.transform.parent = GameObject.Find("Dead Particles").transform;

		Destroy (shot.gameObject, 10.0f);
		Destroy (gameObject);
	}
}

