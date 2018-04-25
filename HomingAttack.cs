using UnityEngine;
using System.Collections;

/*
 * Function that controls the movement of the homing attacks fired by the player
 */

public class HomingAttack : MonoBehaviour {

	public GameObject target; //the destination of the homing attack
	public Rigidbody rb; //the physics body of the projectile

	//vectors involved in the seek steering behaviour
	public Vector3 initialVelocity;
	public Vector3 currentVelocity;
	public Vector3 desiredVelocity;
	public Vector3 desiredDirection;

	public float speed; //how fast the projectile travels

	//initialisation
	void Start () {
		rb = GetComponent<Rigidbody> ();

		rb.velocity = initialVelocity;
		currentVelocity = rb.velocity;
	}

	//calculate the direction the projectile needs to go to make contact
	void seek() {
		//calculate where the target is in relation to this projectile
		//make this vector longer according to the speed
		desiredVelocity = target.transform.position - rb.transform.position;
		desiredVelocity.Normalize ();
		desiredVelocity *= speed;

		//the direction is where the projectile needs to go versus where it is currently going
		desiredDirection = desiredVelocity - currentVelocity;
	}

	void FixedUpdate () {
		//if the target is dead, destroy this projectile to prevent exceptions
		if (target == null) {
			Destroy (gameObject);
		} else {
			seek ();

			//add the desired direction to the velocity taking mass into account
			currentVelocity += (desiredDirection / rb.mass);
			currentVelocity.Normalize ();
			currentVelocity *= speed;

			rb.velocity = currentVelocity;

			//continually decrease the mass and increase the speed up to a point
			rb.mass = rb.mass * 0.92f;
			speed *= 1.005f;
			Mathf.Clamp (speed, 0, 80);

			transform.rotation = Quaternion.LookRotation (currentVelocity);
		}
	}
		
}
