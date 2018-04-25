using UnityEngine;
using System.Collections;

/*
 * Brain script that controls the state of large enemies
 */

public class ParticleEnemyController : MonoBehaviour {
	//the effect played when hit
	public GameObject spray; 

	//the powerup, and if it should drop one on death
	public GameObject powerUp;
	public bool dropsPowerUp;

	//if the enemy is dead
	private bool dead = false;

	//associated scripts
	private EnemyStats es;
	public TextController tc;
	private EffectOnDeath eod;

	//initialisation
	void Start () {
		eod = GetComponent<EffectOnDeath> ();
		es = GetComponent<EnemyStats> ();
		tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();
	}

	//called when the enemy is hit by the player
	public void TakeDamage(float damage) {
		es.health -= damage;
		Instantiate (spray, transform.position, Quaternion.identity);
		if (es.health <= 0) {
			if (!dead) {
				Death ();
			}
		}
	}

	//called when the enemy reaches zero health
	void Death() {
		dead = true;
		eod.SpawnEffect (); //spawn an explosion
		if (dropsPowerUp) {
			Instantiate (powerUp, transform.position, Quaternion.Euler(90, 0, 0));
		}
		tc.UpdateScore (es.score);
		Destroy (gameObject);
	}
}

