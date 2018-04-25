using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * Version of DestroyByContact that originally included a feature where enemies would
 * flash when hit, but did not work properly.
 * 
 */
public class BossHurtByContact : MonoBehaviour {

	//all possible scripts 
	private Boss1Controller boss1Controller;
	private Boss2Controller boss2Controller;
	private PlayerController playerController;

	//initialise scripts into variables
	void Start () {
		boss1Controller = GetComponent<Boss1Controller> ();
		boss2Controller = GetComponent<Boss2Controller> ();
		playerController = GameObject.FindObjectOfType<PlayerController>();
	}

	//called when something enters the collider of the boss
	void OnTriggerEnter(Collider other) {
		//ignore the collider of the boundary
		if (other.tag == "Boundary") {
			return;
		}

		//if the player collides with the boss, and they are not invincible, hurt them
		if (other.tag == "Player") {
			if (!playerController.ps.invincible) {
				playerController.TakeDamage ();
			}
		} 

		//if a bullet collides with the first boss, damage them and delete the bullet
		else if (boss1Controller != null) {
			boss1Controller.TakeDamage (playerController.ps.damage);
			Destroy (other.gameObject);
		} 

		//same as above, for second boss
		else if (boss2Controller != null) {
			boss2Controller.TakeDamage (playerController.ps.damage);
			Destroy (other.gameObject);
		}
	}
}
