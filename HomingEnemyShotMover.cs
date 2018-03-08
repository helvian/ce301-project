using UnityEngine;
using System.Collections;

public class HomingEnemyShotMover : MonoBehaviour {
	private Rigidbody rb;

	public float speed;
	private GameObject player;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		if (player = GameObject.FindGameObjectWithTag ("Player")){
			AimAndMove ();
		}
	}
		
	/* AimAndMove makes the enemy aim their shot at the player and fire in that direction */
	void AimAndMove() {
		Vector3 direction = player.transform.position - transform.position;
		direction.Normalize ();
		Vector3 movement = direction * speed;
		rb.velocity = new Vector3 (movement.x, movement.y, movement.z);
	}

}
