using UnityEngine;
using System.Collections;

public class ParticleEnemyController : MonoBehaviour {
	private Rigidbody rb;

	public GameObject spray;

	public GameObject powerUp;
	public bool dropsPowerUp;
	private bool dead = false;

	private EnemyStats es;
	public TextController tc;

	void Start () {
		es = GetComponent<EnemyStats> ();
		rb = GetComponent<Rigidbody> ();
		tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();
	}

	public void TakeDamage(float damage) {
		Debug.Log ("asdfkjh");
		es.health -= damage;
		Instantiate (spray, transform.position, Quaternion.identity);
		if (es.health <= 0) {
			if (!dead) {
				Death ();
			}
		}
	}

	void Death() {
		dead = true;
		if (dropsPowerUp) {
			Instantiate (powerUp, transform.position, Quaternion.Euler(90, 0, 0));
		}
		tc.UpdateScore (es.score);
		Destroy (gameObject);
	}
}

