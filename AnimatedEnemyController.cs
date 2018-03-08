using UnityEngine;
using System.Collections;

public class AnimatedEnemyController : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;

	public Animator an;
	public bool shootNow = false;

	public GameObject spray;

	private EnemyStats es;
	public TextController tc;

	void Start () {
		es = GetComponent<EnemyStats> ();
		an = GetComponent<Animator> ();
		tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();
	}

	void Update() {
		if (shootNow) {
			if (Time.time > es.nextFire) {
				es.nextFire = Time.time + es.fireRate;
				Instantiate (shot, shotSpawn.position, Quaternion.identity);
			}
		}
	}

	public void TakeDamage(float damage) {
		es.health -= damage;
		Instantiate (spray, transform.position, Quaternion.identity);
		if (es.health <= 0) {
			tc.UpdateScore (es.score);
			Destroy (gameObject);
		}
	}
}

