using UnityEngine;
using System.Collections;

/*
 * Script that handles most collision between player and enemies
 */

public class DestroyByContact : MonoBehaviour {

	//death explosions
	public GameObject explosion;
	public GameObject playerExplosion;

	//verification variable so OnTriggerEnter only triggers its effects once
	private bool dead;

	//all possible controllers this script handles
	private PlayerController playerController;
	private EnemyController enemyController;
	private SideEnemyController sideEnemyController;
	private PowerupController powerupController;
	private ParticleEnemyController particleEnemyController;
	private MissileSeek missileSeek;

	//initialise scripts into variables
	void Start() {
		dead = false;
		playerController = GameObject.FindObjectOfType<PlayerController>();
		powerupController = GameObject.FindObjectOfType<PowerupController> ();
		enemyController = gameObject.GetComponent<EnemyController> ();
		sideEnemyController = gameObject.GetComponent<SideEnemyController> ();
		particleEnemyController = gameObject.GetComponent<ParticleEnemyController> ();
		missileSeek = gameObject.GetComponent<MissileSeek> ();
	}

	//called when anything with this script collides with something else
	//valid collisions are defined by the matrix of physics interactions in Unity's Physics settings
	void OnTriggerEnter(Collider other) {
		//ignore the boundary and lock-on colliders
		if (other.tag == "Boundary") {
			return;
		}
		if (other.tag == "Lockon") {
			return;
		}

		//ignore everything that was already processed as dead
		if (dead) {
			return;
		}

		//if something collides with the player
		if (other.tag == "Player") {
			//increment powerup meter if it was a powerup
			if (gameObject.tag == "Powerup") {
				powerupController.IncrementMeter ();
				Destroy (gameObject);
			} 
			//if it is not a scan box, hurt the player and destroy the other collider
			else if (!playerController.ps.invincible) {
				if (gameObject.GetComponentInChildren<ScanBox> () == null) {
					playerController.TakeDamage ();
					Destroy (gameObject);
				}
			}
		} 
		//all cases for when an enemy is attacked; hurt the respective enemy
		else {
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
