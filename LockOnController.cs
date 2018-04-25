using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script that controls the lock on input function for the player 
 */

public class LockOnController : MonoBehaviour {

	//the circle defining the lock on radius, and if it is drawn on screen
	public Collider lockOnZone;
	public MeshRenderer lockOnZoneVisible;

	//timer to prevent abuse
	public float startWait;

	//how many lockons have been made
	public float lockOnCount = 0;

	//if the lock on is currently on
	public bool lockingOn;

	public GameObject homingProjectile; //the homing projectile itself

	private Text lockonCounter; //the number under the player during lockon

	//associated scripts
	private PlayerStats ps;
	public HomingAttack homingProjectileTarget;
	public GetTargetsInRange tir;

	//initialisation
	void Start() {
		lockOnZone = GameObject.FindGameObjectWithTag ("Lockon").GetComponent<Collider> ();
		lockOnZoneVisible = GameObject.FindGameObjectWithTag ("Lockon").GetComponent<MeshRenderer> ();
		lockonCounter = gameObject.GetComponentInChildren<Text> ();
		ps = GetComponent<PlayerStats> ();
		tir = GetComponent<GetTargetsInRange> ();
	}

	//the function called when the fire button is pressed down
	public void LockOn() {
		//enable a bunch of flags and start locking on
		lockingOn = true;
		lockOnZone.enabled = true;
		lockOnZoneVisible.enabled = true;
		lockonCounter.enabled = true;
		lockOnCount = 0;
		StartCoroutine (AcquireLockOn ());
	}

	//the function called when the fire button is released
	public bool Fire() {
		//disable the flags raised in LockOn() and stop locking on
		lockingOn = false;
		lockOnZone.enabled = false;
		lockOnZoneVisible.enabled = false;
		StopCoroutine (AcquireLockOn ());

		//for every target that was found during lock on
		foreach (Collider c in tir.targetsInRange) {
			//access their LockOnTarget script attached to them
			LockOnTarget l = c.gameObject.GetComponent<LockOnTarget> ();

			//for every lock on mark they have, create a projectile and direct it towards them
			for (int i = 0; i < l.lockedOn; i++) {
				GameObject thisProjectile = Instantiate (homingProjectile, gameObject.transform.position + Random.insideUnitSphere, Quaternion.identity);
				homingProjectileTarget = thisProjectile.GetComponent<HomingAttack> ();
				homingProjectileTarget.target = c.gameObject;
			}
			//clear their marks
			l.lockedOn = 0;
		}
		//two failsafes that reset the variables and hide the UI elements regarding lock on weapon
		if (lockOnCount > 0) {
			lockOnCount = 0;
			lockonCounter.text = lockOnCount.ToString();
			lockonCounter.enabled = false;
			return true;
		} else {
			lockOnCount = 0;
			lockonCounter.text = lockOnCount.ToString();
			lockonCounter.enabled = false;
			return false;
		}
	}

	//coroutine that finds enemies
	IEnumerator AcquireLockOn() {
		yield return new WaitForSeconds (startWait);

		//while the number of marks applied is under the maximum and the button is still held down
		while (lockOnCount < ps.lockOnMax && lockingOn) {

			//scan for targets
			tir.ScanInSphere ();

			//if targets were found
			if (tir.targetsInRange.Count > 0) {

				//apply a mark to all targets found
				foreach (Collider c in tir.targetsInRange) {
					if (lockOnCount < ps.lockOnMax) {
						LockOnTarget l = c.gameObject.GetComponent<LockOnTarget> ();
						l.lockedOn++;
						lockOnCount++;
					}
				};
			}

			//update text, then wait
			lockonCounter.text = lockOnCount.ToString();
			yield return new WaitForSeconds (ps.lockOnRate);
		}
		yield break;
	}
}
