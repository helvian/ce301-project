using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script that plays a given particle system upon the death of an object 
 * with this script attached
 */

public class EffectOnDeath : MonoBehaviour {

	//effect to play on death
	public ParticleSystem deathEffect;

	//parent object to attach effect to for organisation
	public GameObject effectParent;

	//initialise effectParent
	void Start() {
		effectParent = GameObject.Find ("Death Effects");
	}

	//called when the object this script is attached to dies
	public void SpawnEffect() {
		ParticleSystem temp = Instantiate (deathEffect, gameObject.transform.position, gameObject.transform.rotation);
		temp.transform.parent = effectParent.transform;
	}
}
