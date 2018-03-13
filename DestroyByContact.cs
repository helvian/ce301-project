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
	private ParticleEnemyController particleEnemyController;
	private MissileSeek missileSeek;

	void Start() {
		dead = false;
		playerController = GameObject.FindObjectOfType<PlayerController>();
		powerupController = GameObject.FindObjectOfType<PowerupController> ();
		enemyController = gameObject.GetComponent<EnemyController> ();
		sideEnemyController = gameObject.GetComponent<SideEnemyController> ();

		particleEnemyController = gameObject.GetComponent<ParticleEnemyController> ();
		missileSeek = gameObject.GetComponent<MissileSeek> ();
	}

	void OnTriggerEnter(Collider other) {
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
				if (gameObject.GetComponentInChildren<ScanBox> () == null) {
					playerController.TakeDamage ();
					Destroy (gameObject);
				}
			}
		} else {
			if (enemyController != null) {
				Debug.Log("name: " + gameObject.name);
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
