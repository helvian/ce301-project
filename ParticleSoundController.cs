using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ParticleSoundController : MonoBehaviour {

	private AudioSource sound;
	private ParticleSystem thisSystem;

	//private int particlesLastFrame;
	//private int particlesThisFrame;

	private float interval;
	private float nextSound = 0f;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource> ();
		thisSystem = GetComponent<ParticleSystem> ();

		interval = thisSystem.main.duration;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (nextSound > interval && thisSystem.particleCount > 0 && thisSystem.isEmitting) {
			nextSound = 0;
			sound.PlayOneShot (sound.clip);
		}
		nextSound += Time.deltaTime;
		/*
		particlesThisFrame = thisSystem.particleCount;
		if (particlesLastFrame < particlesThisFrame) {
			sound.PlayOneShot(sound.clip);
		}
		particlesLastFrame = particlesThisFrame;*/
	}
}
