using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Vector3 Camera1;
	public Vector3 Camera2;
	private bool isTop;
	private int cameraDelay;
	// Use this for initialization
	void Start (){
		isTop = true;
		cameraDelay = 500;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire2") && isTop == true) {
			Camera.main.transform.position = Camera1;
			isTop = false;
		} else if (Input.GetButton ("Fire2") && isTop == false) {
			Camera.main.transform.position = Camera2;
			isTop = true;
		}
	}
}
