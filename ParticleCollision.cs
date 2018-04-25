using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles all particles that can collide with the player
 * 
 */

public class ParticleCollision : MonoBehaviour {

	private ParticleSystem particle; //the particle system this script is attached to

	private PlayerController pc; //the player

	List<ParticleSystem.Particle> enter; //the list of particles that are colliding with the player

	//initialisation
	void Awake () {
		particle = GetComponent<ParticleSystem> ();	
		enter = new List<ParticleSystem.Particle>();
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}

	//this is called every frame for some reason
	//if any particle has contacted with the collider they are seeking, damage the player
	void OnParticleTrigger(){
		int numEnter = particle.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
		if (numEnter > 0) {
			if (!pc.ps.invincible) {
				pc.TakeDamage ();
			}
		}
	}

}
