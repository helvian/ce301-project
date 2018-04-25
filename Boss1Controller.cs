using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * Brains for the first boss
 * Controls the weaponry and movement, and health
 *
 */

public class Boss1Controller : MonoBehaviour {

	public GameObject[] shot; //the weapons attached to the ship
	public Transform[] shotSpawn; //the places that missiles can spawn from
	public ParticleSystem[] particleShots; //the polarity weapons attached to the ship
	public int missileCounter; //counter used to time missile launches

	public GameObject[] turrets; //the turret models on the ship

	public bool facingLeft = true; //the direction the boss is facing

	public GameObject player; //the player
	public Animation clip; //the animation responsible for the boss' initial appearance
	public ParticleSystem deathExplosions; //the particles that appear when the boss dies
	public Slider healthBar; //the health bar at the top of the screen

	//other scripts attached to the object
	private BossStats bs;
	private Boss1Movement bm;
	private EffectOnDeath eod;
	private GameController gc;

	//play the intro animation, assign scripts to variables


	void Start () {
		clip.Play ();
		bs = GetComponent<BossStats> ();
		bm = GetComponent<Boss1Movement> ();
		eod = GetComponent<EffectOnDeath> ();
		gc = GameObject.Find ("Game Controller").GetComponent<GameController> ();

		//begin coroutines if the player is alive
		if (player = GameObject.FindGameObjectWithTag ("Player")){
			StartCoroutine (AimTurrets ());
			StartCoroutine (BossAttacks ());
			StartCoroutine (bm.Hover ());
		}

		//initialise the health bar
		bs.health = bs.maxHealth;
		healthBar = GameObject.Find ("Boss Health Bar").GetComponent<Slider>();
		healthBar.value = bs.health;
	}

	//called when the enemy is hit
	public void TakeDamage(float damage) {
		if (bs.health > 0) {
			bs.health -= damage;
			healthBar.value = bs.health;

			//transition into the second phase at 50% health or less
			if (bs.health <= bs.maxHealth / 2) {
				if (bs.phase != 2) {
					bs.phase = 2;
				}
			}

			//begin death and stop all functions at 0 health
			if (bs.health <= 0) {
				StopCoroutine (AimTurrets ());
				StopCoroutine (BossAttacks ());
				for (int i = 0; i < particleShots.Length; i++) {
					particleShots [i].Stop ();
				}
				StartCoroutine (DeathAnimation ());
			}
		}
	}
		
	//rotate the turrets nearest to the player so that they face the player at all times
	//rotate the turrets furthest from the player so that they face parallel to the boss' direction
	IEnumerator AimTurrets() {
		while (true) {
			if (bs.phase == 1) {
				turrets [0].transform.LookAt (player.transform.position);
				turrets [1].transform.LookAt (player.transform.position);
				turrets [2].transform.rotation = Quaternion.Euler (0, 90, 0);
				turrets [3].transform.rotation = Quaternion.Euler (0, 90, 0);
			} else if (bs.phase == 2) {
				turrets [0].transform.rotation = Quaternion.Euler (0, 90, 0);
				turrets [1].transform.rotation = Quaternion.Euler (0, 90, 0);
				turrets [2].transform.LookAt (player.transform.position);
				turrets [3].transform.LookAt (player.transform.position);
			}
			yield return new WaitForEndOfFrame ();
		}
	}

	//choreographing of which weapons fire when and when missiles fire
	IEnumerator BossAttacks() {
		particleShots [0].Play (); //begin the firing of this weapon
		particleShots [1].Play ();
		particleShots [4].Play ();
		while (bs.phase == 1) {
			//every seven seconds, fire a missile
			if (facingLeft) {
				missileCounter++;
			}
			if (missileCounter > 10) {
				Instantiate (shot [1], transform.position, Quaternion.Euler (0, 180, 0));
				missileCounter = 0;
			}
			yield return new WaitForSeconds (0.7f);
		}
		//reinitialise and change the active weapons
		missileCounter = 0;
		particleShots [0].Stop ();
		particleShots [1].Stop ();
		particleShots [4].Stop ();
		particleShots [2].Play ();
		particleShots [3].Play ();
		particleShots [5].Play ();
		while (bs.phase == 2) {
			//every six seconds, fire a missile
			missileCounter++;
			if (missileCounter > 6) {
				Instantiate (shot [1], transform.position, Quaternion.Euler (0, 180, 0));
				missileCounter = 0;
			}
			yield return new WaitForSeconds (1f);
		}
	}

	//called on death
	IEnumerator DeathAnimation(){
		deathExplosions.Play (); //show death effects
		yield return new WaitForSeconds (deathExplosions.main.duration); //wait the duration of the particle cycle
		eod.SpawnEffect (); //spawn explosion
		gc.boss1Dead = true; //signal that the boss is dead to the game's brain
		Destroy (gameObject); //delete the object
	}
}
