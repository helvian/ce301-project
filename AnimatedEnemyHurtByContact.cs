using UnityEngine;
using System.Collections;

public class AnimatedEnemyHurtByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;

	public int scoreValue;
	private bool dead;

	private PlayerController playerController;
	private AnimatedEnemyController aec;


	void Start() {
		dead = false;
		playerController = GameObject.FindObjectOfType<PlayerController>();
		aec = GameObject.FindObjectOfType<AnimatedEnemyController> ();

	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary") {
			return;
		}
		if (dead) {
			return;
		}
		if (other.tag == "Player") {
			if (!playerController.ps.invincible) {
				playerController.TakeDamage ();
				Destroy (gameObject);
			}
		} else {
			aec.TakeDamage (playerController.ps.damage);
			Destroy (other.gameObject);
		}
	}


}
