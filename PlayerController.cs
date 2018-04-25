using UnityEngine;
using System.Collections;

/*
 * Brain script for the player and its colliders, where it can move to
 */

//the boundary on the screen where the player cannot move beyond
[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

//the available weapons
public enum Weapon {
	Gatling, LockOn
};

public class PlayerController : MonoBehaviour
{
	private Rigidbody rb; //the physics body attached to the player
	public AudioSource[] au; //the sounds the player can make

	//the polarity colliders on the player
	Collider darkP;
	Collider lightP;
	Light darkPL;
	Light lightPL;

	public Boundary boundary;

	//the projectiles and where they can appear
	public GameObject shot;
	public Transform[] shotSpawn;

	//which weapon is selected
	public Weapon weap;

	//invincibility variables for timing and effects
	public float invTimer;
	public bool timerOn;
	public ParticleSystem invFX;

	//associated scripts
	public PlayerStats ps;
	public TextController tc;
	private PowerupController pc;
	private LockOnController loc;

	//initialisation
	void Start ()
	{
		weap = Weapon.Gatling;
		ps = GetComponent<PlayerStats> ();
		ps.invincible = true;
		ps.numShots = 2;
		rb = GetComponent<Rigidbody> ();
		au = GetComponents<AudioSource> ();
		tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();
		pc = GameObject.FindGameObjectWithTag ("Powerup Controller").GetComponent<PowerupController> ();
		loc = GetComponent<LockOnController> ();
		darkP = GameObject.FindGameObjectWithTag ("Dark Polarity").GetComponent<Collider>();
		lightP = GameObject.FindGameObjectWithTag ("Light Polarity").GetComponent<Collider>();
		darkPL = GameObject.FindGameObjectWithTag ("Dark Polarity").GetComponent<Light>();
		lightPL = GameObject.FindGameObjectWithTag ("Light Polarity").GetComponent<Light>();
	}

	void Update () {
		//gatling weapon firing
		if (weap == Weapon.Gatling) {
			//if the fire button is held down and enough time has passed since the last shot, fire and make noise
			if (Input.GetButton ("Fire1") && Time.time > ps.nextFire) {
				ps.nextFire = Time.time + ps.fireRate;
				for (int i = 0; i < ps.numShots; i++) {
					Instantiate (shot, shotSpawn [i].position, Quaternion.identity);
				}
				au[0].PlayOneShot (au[0].clip);
			}
		} 
		//homing weapon firing
		else if (weap == Weapon.LockOn) {
			//begin locking onto targets on button press
			if (Input.GetButtonDown ("Fire1") && Time.time > ps.nextHoming) { 	
				ps.nextHoming = Time.time + ps.homingCD;
				loc.LockOn ();
			}
			//begin firing onto targets on button release
			if (Input.GetButtonUp ("Fire1")) {
				if (loc.Fire ()) {
					ps.nextHoming = Time.time + ps.homingCD;
					au [1].PlayOneShot (au [1].clip);
				};
			}
		}
		//if the player has powerups, let them cash in their powerups
		if (Input.GetButtonDown ("Jump") && pc.powerupsHeld > 0) {
			pc.CashInPowerups ();
		}
		//change the polarity of the ship
		if (Input.GetButtonDown ("Fire2")) {
			InvertPolarity ();
		}
		//change the weapon of the ship
		if (Input.GetButtonDown ("Fire3") && !Input.GetButton("Fire1")) {
			ChangeWeapon ();
		}
		//read for invincibility 
		if (ps.invincible && !timerOn) {
			BecomeInvincible ();
		}
	}

	//coroutine that controls the disabling of invincibility
	IEnumerator InvincibilityTimer() {
		timerOn = true;
		yield return new WaitForSeconds (3);
		ps.invincible = false;
		timerOn = false;

		StopCoroutine (InvincibilityTimer ());
	}

	//movement handled here
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * ps.speed;

		//disallow movement outside of the defined boundary
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax));

		//tilt the ship along the direction of movement
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -ps.tilt);
	}

	//called when the player collides with anything that harms them
	public void TakeDamage() {
		ps.health--;
		tc.UpdateHealth(ps.health); //update the health text
		if (!timerOn) {
			BecomeInvincible ();
		}

		//death
		if (ps.health <= 0) {
			gameObject.SetActive (false);
			tc.ShowGameOver ();
		}
	}

	//invoke invincibility and start the timer
	void BecomeInvincible() {
		invFX.Play ();
		ps.invincible = true;
		timerOn = true;
		StartCoroutine (InvincibilityTimer());
	}

	//disable the active collider and effect, enable the inactive ones
	void InvertPolarity() {
		switch (lightP.enabled) {
		case true:
			lightP.enabled = false;
			lightPL.enabled = false;
			darkP.enabled = true;
			darkPL.enabled = true;
			break;
		case false:
			darkP.enabled = false;
			darkPL.enabled = false;
			lightP.enabled = true;
			lightPL.enabled = true;
			break;
		}
	}

	//cycle through the weapons and change the active one
	void ChangeWeapon() {
		if (weap == Weapon.Gatling) {
			weap++;
		} else {
			weap = Weapon.Gatling;
		}
		tc.UpdateWeapon (weap.ToString ());

	}
}
