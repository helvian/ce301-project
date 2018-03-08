using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles all particles that must collide with the player
 * 
 */

public class ParticleCollision : MonoBehaviour {

	private ParticleSystem particle;
	private PlayerController pc;
	List<ParticleSystem.Particle> enter;
	
	void Awake () {
		particle = GetComponent<ParticleSystem> ();	
		enter = new List<ParticleSystem.Particle>();
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}

	//this is called every frame for some reason
	void OnParticleTrigger(){
		int numEnter = particle.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
		if (numEnter > 0) {
			if (!pc.ps.invincible) {
				pc.TakeDamage ();
			}
		}
	}

}
