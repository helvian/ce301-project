using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * Brains for the second boss
 * Controls the weaponry and movement, and health
 *
 */
public class Boss2Controller : MonoBehaviour {

	public ParticleSystem[] particleShots; //weapons attached to the ship

	public GameObject player; //the player object
	public GameObject spawn; //where the boss appears
	public Animator an; //the animator object
	public ParticleSystem deathExplosions; //the effects that play on death
	public Slider healthBar; //the health bar at the top of the screen

	//relevant scripts attached to the object
	private BossStats bs;
	private EffectOnDeath eod;

	//initialise scripts and variables
	void Start () {
		an = GetComponent<Animator> ();
		bs = GetComponent<BossStats> ();
		eod = GetComponent<EffectOnDeath> ();
		spawn = GameObject.Find ("Projectiles");
		//start firing only if the player is alive
		if (player = GameObject.FindGameObjectWithTag ("Player")){
			StartCoroutine (BossAttacks ());
		}
		//initialise health bar
		bs.health = bs.maxHealth;
		healthBar = GameObject.Find ("Boss Health Bar").GetComponent<Slider>();
		healthBar.value = bs.health;
	}

	//called when enemy is hit
	public void TakeDamage(float damage) {
		if (bs.health > 0) {
			bs.health -= damage;
			healthBar.value = bs.health;

			//transition into next phase below 25% health
			if (bs.health <= bs.maxHealth / 4) {
				if (bs.phase != 2) {
					bs.phase = 2;
				}
			}

			//begin death routines at 0 health and after the phase has transitioned properly
			if (bs.health <= 0 && bs.phase == 2) {
				for (int i = 0; i < particleShots.Length; i++) {
					particleShots [i].Stop ();
				}
				StartCoroutine (DeathAnimation ());
			}
		}
	}

	//choreograph which weapons fire when
	IEnumerator BossAttacks() {
		particleShots [0].Play ();
		particleShots [1].Play ();

		//infinite loop while the boss is still in phase 1
		while (bs.phase == 1) {
			yield return new WaitForSeconds (1);
		}

		//signal animator to play the next animation
		an.SetBool ("Phase 2", true);

		particleShots [0].Stop ();
		particleShots [1].Stop ();
		particleShots [2].Play ();
		particleShots [3].Play ();
	}

	//played when enemy reaches zero health
	IEnumerator DeathAnimation(){
		deathExplosions.Play (); 
		yield return new WaitForSeconds (deathExplosions.main.duration);
		eod.SpawnEffect ();
		Destroy (gameObject);
	}
}

