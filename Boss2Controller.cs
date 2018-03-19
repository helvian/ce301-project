using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Boss2Controller : MonoBehaviour {

	public GameObject[] shot;
	public Transform[] shotSpawn;
	public ParticleSystem[] particleShots;

	public GameObject player;
	public GameObject spawn;
	public Animator an;
	public ParticleSystem deathExplosions;
	public Slider healthBar;

	private BossStats bs;
	private EffectOnDeath eod;

	// Use this for initialization
	void Start () {
		an = GetComponent<Animator> ();
		bs = GetComponent<BossStats> ();
		eod = GetComponent<EffectOnDeath> ();
		spawn = GameObject.Find ("Projectiles");
		if (player = GameObject.FindGameObjectWithTag ("Player")){
			StartCoroutine (BossAttacks ());
		}
		bs.health = bs.maxHealth;
		healthBar = GameObject.Find ("Boss Health Bar").GetComponent<Slider>();
		healthBar.value = bs.health;
	}

	public void TakeDamage(float damage) {
		if (bs.health > 0) {
			bs.health -= damage;
			healthBar.value = bs.health;
			if (bs.health <= bs.maxHealth / 4) {
				if (bs.phase != 2) {
					bs.phase = 2;
				}
			}
			if (bs.health <= 0 && bs.phase == 2) {
				for (int i = 0; i < particleShots.Length; i++) {
					particleShots [i].Stop ();
				}
				StartCoroutine (DeathAnimation ());
			}
		}
	}

	IEnumerator BossAttacks() {
		particleShots [0].Play ();
		particleShots [1].Play ();
		while (bs.phase == 1) {
			yield return new WaitForSeconds (1);
		}
		an.SetBool ("Phase 2", true);
		particleShots [0].Stop ();
		particleShots [1].Stop ();
		particleShots [2].Play ();
		particleShots [3].Play ();
	}

	IEnumerator DeathAnimation(){
		deathExplosions.Play ();
		yield return new WaitForSeconds (deathExplosions.main.duration);
		eod.SpawnEffect ();
		Destroy (gameObject);
	}
}

