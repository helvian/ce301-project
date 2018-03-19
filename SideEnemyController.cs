using UnityEngine;
using System.Collections;

public class SideEnemyController : MonoBehaviour {
	private Rigidbody rb;

	public ParticleSystem shot;
	//public GameObject shot;
	//public Transform shotSpawn;

	public Animator an;
	public bool shootNow = false;
	private bool dead;

	public GameObject spray;

	private EnemyStats es;
	private EffectOnDeath eod;
	public TextController tc;

	public ScanBox sb;

	void Start () {
		es = GetComponent<EnemyStats> ();
		eod = GetComponent<EffectOnDeath> ();
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * es.speed;
		sb = GetComponent<ScanBox> ();
		tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();
	}

	public void Fire() {
		shot.Play ();
	}

	/*void Update() {
		if (sb.OnTriggerEnter()) {
			if (Time.time > es.nextFire) {
				es.nextFire = Time.time + es.fireRate;
				Instantiate (shot, shotSpawn.position, Quaternion.identity);
			}
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
		tc.UpdateScore (es.score);
		shot.Stop ();
		Vector3 tempVec = shot.transform.lossyScale;
		Debug.Log (shot.transform.lossyScale);
		tempVec.x = 1 / shot.transform.parent.lossyScale.x;
		tempVec.y = 1 /shot.transform.parent.lossyScale.y;
		tempVec.z = 1 /shot.transform.parent.lossyScale.z;
		shot.transform.localScale = tempVec;
		shot.transform.parent = GameObject.Find("Dead Particles").transform;
		Destroy (shot);
		Destroy (shot.gameObject, 10.0f);
		Destroy (gameObject);
	}
}

