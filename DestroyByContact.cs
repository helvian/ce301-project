using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	
	public GameObject explosion;
	public GameObject playerExplosion;

	private bool dead;

	private PlayerController playerController;
	private EnemyController enemyController;
	private SideEnemyController sideEnemyController;
	private PowerupController powerupController;
	public ParticleEnemyController particleEnemyController;
	private MissileSeek missileSeek;

	void Start() {
		dead = false;
		playerController = GameObject.FindObjectOfType<PlayerController>();
		enemyController = GameObject.FindObjectOfType<EnemyController> ();
		sideEnemyController = GameObject.FindObjectOfType<SideEnemyController> ();
		powerupController = GameObject.FindObjectOfType<PowerupController> ();
		particleEnemyController = GameObject.FindObjectOfType<ParticleEnemyController> ();
		missileSeek = GameObject.FindObjectOfType<MissileSeek> ();
	}

	void OnTriggerEnter(Collider other) {
		if (gameObject.GetComponentInChildren<ScanBox> () != null) {
			return;
		}
		if (other.tag == "Boundary") {
			return;
		}
		if (other.tag == "Lockon") {
			return;
		}
		if (dead) {
			return;
		}
		if (other.tag == "Player") {
			if (gameObject.tag == "Powerup") {
				powerupController.IncrementMeter ();
				Destroy (gameObject);
			} 
			else if (!playerController.ps.invincible) {
				playerController.TakeDamage ();
				Destroy (gameObject);
			}
		} else {
			if (enemyController != null) {
				enemyController.TakeDamage (playerController.ps.damage);
				Destroy (other.gameObject);
			} else if (sideEnemyController != null) {
				sideEnemyController.TakeDamage (playerController.ps.damage);
				Destroy (other.gameObject);
			} else if (particleEnemyController != null) {
				particleEnemyController.TakeDamage (playerController.ps.damage);
				Destroy (other.gameObject);
			} else if (missileSeek != null) {
				missileSeek.TakeDamage (playerController.ps.damage);
				Destroy (other.gameObject);
			}
		}
	}

}
