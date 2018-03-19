using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnDeath : MonoBehaviour {

	public ParticleSystem deathEffect;
	public GameObject effectParent;

	void Start() {
		effectParent = GameObject.Find ("Death Effects");
	}

	public void SpawnEffect() {
		ParticleSystem temp = Instantiate (deathEffect, gameObject.transform.position, gameObject.transform.rotation);
		temp.transform.parent = effectParent.transform;
	}
}
