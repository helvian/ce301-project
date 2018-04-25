using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*
 * Script that controls when sounds should play for specific particle weapons
 */

public class ParticleSoundController : MonoBehaviour {

	private AudioSource sound; //the sound to play
	private ParticleSystem thisSystem; //the particle system this script is attached to

	private float interval; //how often to play sounds
	private float nextSound = 0f; 

	//initialisation
	void Start () {
		sound = GetComponent<AudioSource> ();
		thisSystem = GetComponent<ParticleSystem> ();

		interval = thisSystem.main.duration; //read the particle system's duration for the interval
	}
		
	void FixedUpdate () {
		//if enough time has passed since the last sound, and this system has particles in existence
		//and it is activated, play a sound
		if (nextSound > interval && thisSystem.particleCount > 0 && thisSystem.isEmitting) {
			nextSound = 0;
			sound.PlayOneShot (sound.clip);
		}
		nextSound += Time.deltaTime;
	}
}
