using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Boss1Controller : MonoBehaviour {

	public GameObject[] shot;
	public Transform[] shotSpawn;
	public ParticleSystem[] particleShots;
	public int missileCounter;

	public GameObject[] turrets;

	public bool facingLeft = true;

	public GameObject player;
	public GameObject spawn;
	public Animation clip;
	public ParticleSystem deathExplosions;
	public Slider healthBar;

	private BossStats bs;
	private Boss1Movement bm;
	private EffectOnDeath eod;
	private GameController gc;

	// Use this for initialization
	void Start () {
		clip.Play ();
		bs = GetComponent<BossStats> ();
		bm = GetComponent<Boss1Movement> ();
		eod = GetComponent<EffectOnDeath> ();
		gc = GameObject.Find ("Game Controller").GetComponent<GameController> ();
		spawn = GameObject.Find ("Projectiles");
		if (player = GameObject.FindGameObjectWithTag ("Player")){
			StartCoroutine (AimTurrets ());
			StartCoroutine (BossAttacks ());
			StartCoroutine (bm.Hover ());
		}
		bs.health = bs.maxHealth;
		healthBar = GameObject.Find ("Boss Health Bar").GetComponent<Slider>();
		healthBar.value = bs.health;
	}
	
	public void TakeDamage(float damage) {
		if (bs.health > 0) {
			bs.health -= damage;
			healthBar.value = bs.health;
			if (bs.health <= bs.maxHealth / 2) {
				if (bs.phase != 2) {
					bs.phase = 2;
				}
			}
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

	IEnumerator BossAttacks() {
		particleShots [0].Play ();
		particleShots [1].Play ();
		particleShots [4].Play ();
		while (bs.phase == 1) {
			if (facingLeft) {
				missileCounter++;
			}
			if (missileCounter > 10) {
				Instantiate (shot [1], transform.position, Quaternion.Euler (0, 180, 0));
				missileCounter = 0;
			}
			yield return new WaitForSeconds (0.7f);
		}
		missileCounter = 0;
		particleShots [0].Stop ();
		particleShots [1].Stop ();
		particleShots [4].Stop ();
		particleShots [2].Play ();
		particleShots [3].Play ();
		particleShots [5].Play ();
		while (bs.phase == 2) {
			missileCounter++;
			if (missileCounter > 6) {
				Instantiate (shot [1], transform.position, Quaternion.Euler (0, 180, 0));
				missileCounter = 0;
			}
			yield return new WaitForSeconds (1f);
		}
	}

	IEnumerator DeathAnimation(){
		deathExplosions.Play ();
		yield return new WaitForSeconds (deathExplosions.main.duration);
		eod.SpawnEffect ();
		gc.boss1Dead = true;
		Destroy (gameObject);
	}
}
