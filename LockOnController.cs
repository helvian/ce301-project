using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOnController : MonoBehaviour {

	public Collider lockOnZone;
	public MeshRenderer lockOnZoneVisible;
	public float lockOnRate;
	public float startWait;
	public float lockOnCount = 0;
	public float lockOnMax = 20;
	public bool lockingOn;
	public GetTargetsInRange tir;
	public GameObject homingProjectile;

	public HomingAttack homingProjectileTarget;
	private HashSet<GameObject> lockedOnTargets;
	private Text lockonCounter;

	void Start() {
		lockOnZone = GameObject.FindGameObjectWithTag ("Lockon").GetComponent<Collider> ();
		lockOnZoneVisible = GameObject.FindGameObjectWithTag ("Lockon").GetComponent<MeshRenderer> ();
		lockonCounter = gameObject.GetComponentInChildren<Text> ();
		tir = GetComponent<GetTargetsInRange> ();
	}

	public void LockOn() {
		lockingOn = true;
		Debug.Log ("lockon start");
		lockOnZone.enabled = true;
		lockOnZoneVisible.enabled = true;
		lockonCounter.enabled = true;
		lockOnCount = 0;
		StartCoroutine (AcquireLockOn ());
	}

	public bool Fire() {
		lockingOn = false;
		Debug.Log ("lockon end");
		lockOnZone.enabled = false;
		lockOnZoneVisible.enabled = false;
		StopCoroutine (AcquireLockOn ());
		foreach (Collider c in tir.targetsInRange) {
			LockOnTarget l = c.gameObject.GetComponent<LockOnTarget> ();
			for (int i = 0; i < l.lockedOn; i++) {
				GameObject thisProjectile = Instantiate (homingProjectile, gameObject.transform.position + Random.insideUnitSphere, Quaternion.identity);
				homingProjectileTarget = thisProjectile.GetComponent<HomingAttack> ();
				homingProjectileTarget.target = c.gameObject;
			}
			l.lockedOn = 0;
		}
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

	IEnumerator AcquireLockOn() {
		yield return new WaitForSeconds (startWait);
		while (lockOnCount < lockOnMax && lockingOn) {
			tir.ScanInSphere ();
			if (tir.targetsInRange.Count > 0) {
				foreach (Collider c in tir.targetsInRange) {
					if (lockOnCount < lockOnMax) {
						LockOnTarget l = c.gameObject.GetComponent<LockOnTarget> ();
						l.lockedOn++;
						lockOnCount++;
					}
				};
			}
			lockonCounter.text = lockOnCount.ToString();
			yield return new WaitForSeconds (lockOnRate);
		}
		yield break;
	}
}
