using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Used for particle system based weaponry 
 * Finds the light polarity collider of the player ship and sets it as the object to collide all particles with
 */

public class GetLightCollider : MonoBehaviour {

	public ParticleSystem ps;

	void Start () {
		ps = GetComponent<ParticleSystem> ();
		Collider player = GameObject.FindGameObjectWithTag ("Light Polarity").GetComponent<Collider> ();
		ps.trigger.SetCollider (0, player);
	}


}