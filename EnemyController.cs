using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private Rigidbody rb;

	public ParticleSystem shot;
	//public GameObject shot;
	//public Transform shotSpawn;

	public Animator an;
	public bool shootNow = false;

	public GameObject spray;

	public GameObject powerUp;
	public bool dropsPowerUp;
	private bool dead = false;

	private EnemyStats es;
	private EffectOnDeath eod;
	public TextController tc;

	void Start () {
		es = GetComponent<EnemyStats> ();
		eod = GetComponent<EffectOnDeath> ();
		rb = GetComponent<Rigidbody> ();
		shot = GetComponentInChildren<ParticleSystem> ();
		rb.velocity = transform.forward * es.speed;
		tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();
	}

	/*void Update() {
		if (Time.time > es.nextFire) {
				es.nextFire = Time.time + es.fireRate;
				Instantiate (shot, shotSpawn.position, Quaternion.identity);
		}
	}*/

	public void TakeDamage(float damage) {
		es.health -= damage;
		Instantiate (spray, transform.position, Quaternion.identity);
		if (es.health <= 0) {
			if (!dead) {
				Death ();
			}
		}
	}

	void Death() {
		eod.SpawnEffect ();
		dead = true;
		if (dropsPowerUp) {
			Instantiate (powerUp, transform.position, Quaternion.Euler(90, 0, 0));
		}
		tc.UpdateScore (es.score);
		shot.Stop ();
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

