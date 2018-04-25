using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A script attached to all enemies that holds their lock on marks
 */

public class LockOnTarget : MonoBehaviour {

	public int lockedOn; //how many marks are applied
	public GameObject lockOnImage; //the target reticle attached to the enemy
	private MeshRenderer lockOnRenderer; //the module that draws it on screen

	//initialisation
	void Start(){
		lockedOn = 0;
		lockOnRenderer = lockOnImage.GetComponent<MeshRenderer> ();
	}

	//if the enemy has at least one mark, draw the targeting reticle, otherwise hide it
	void Update(){
		if (lockedOn > 0) {
			lockOnRenderer.enabled = true;
		} else {
			lockOnRenderer.enabled = false;
		}
	}
}
