using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTarget : MonoBehaviour {

	public int lockedOn;
	public GameObject lockOnImage;
	private MeshRenderer lockOnRenderer;

	void Start(){
		lockedOn = 0;
		lockOnRenderer = lockOnImage.GetComponent<MeshRenderer> ();
	}

	void Update(){
		if (lockedOn > 0) {
			lockOnRenderer.enabled = true;
		} else {
			lockOnRenderer.enabled = false;
		}
	}
}
