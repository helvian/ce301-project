using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public enum Weapon {
	Gatling, LockOn
};

public class PlayerController : MonoBehaviour
{
	private Rigidbody rb;
	public AudioSource[] au;
	Collider darkP;
	Collider lightP;
	Light darkPL;
	Light lightPL;

	public Boundary boundary;

	public GameObject shot;
	public Transform[] shotSpawn;
	public Weapon weap;

	public float invTimer;
	public bool timerOn;
	public ParticleSystem invFX;

	public PlayerStats ps;
	public TextController tc;
	private PowerupController pc;
	private LockOnController loc;

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
		if (weap == Weapon.Gatling) {
			if (Input.GetButton ("Fire1") && Time.time > ps.nextFire) {
				ps.nextFire = Time.time + ps.fireRate;
				for (int i = 0; i < ps.numShots; i++) {
					Instantiate (shot, shotSpawn [i].position, Quaternion.identity);
				}
				au[0].PlayOneShot (au[0].clip);
			}
		} else if (weap == Weapon.LockOn) {
			if (Input.GetButtonDown ("Fire1") && Time.time > ps.nextHoming) { 	
				ps.nextHoming = Time.time + ps.homingCD;
				loc.LockOn ();
			}
			if (Input.GetButtonUp ("Fire1")) {
				if (loc.Fire ()) {
					ps.nextHoming = Time.time + ps.homingCD;
					au [1].PlayOneShot (au [1].clip);
				};
			}
		}
		if (Input.GetButtonDown ("Jump") && pc.powerupsHeld > 0) {
			pc.CashInPowerups ();
		}
		if (Input.GetButtonDown ("Fire2")) {
			InvertPolarity ();
		}
		if (Input.GetButtonDown ("Fire3") && !Input.GetButton("Fire1")) {
			ChangeWeapon ();
		}
		if (ps.invincible && !timerOn) {
			BecomeInvincible ();
		}
	}

	IEnumerator InvincibilityTimer() {
		timerOn = true;
		yield return new WaitForSeconds (3);
		ps.invincible = false;
		timerOn = false;

		StopCoroutine (InvincibilityTimer ());
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * ps.speed;

		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax));

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -ps.tilt);
	}

	public void TakeDamage() {
		ps.health--;
		tc.UpdateHealth(ps.health);
		if (!timerOn) {
			BecomeInvincible ();
		}
		if (ps.health <= 0) {
			gameObject.SetActive (false);
			tc.ShowGameOver ();
		}
	}

	void BecomeInvincible() {
		invFX.Play ();
		ps.invincible = true;
		timerOn = true;
		StartCoroutine (InvincibilityTimer());
	}

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

	void ChangeWeapon() {
		if (weap == Weapon.Gatling) {
			weap++;
		} else {
			weap = Weapon.Gatling;
		}
		tc.UpdateWeapon (weap.ToString ());

	}
}
