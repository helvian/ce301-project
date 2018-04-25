using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handle the movement of the first boss independent of the animations
 */

public class Boss1Movement : MonoBehaviour {

	public Vector3 rightAnchor; //point on the right to move to
	public Vector3 leftAnchor; //point on the left to move to
	public Vector3 farLeftAnchor; //point off screen to move to
	public Vector3 centerAnchor; //point in the center to move to
	public float offset; //how far from anchors to move to
	public bool movingLeft = false; //which way the ship is moving
	public float speed = 0.02f; //how fast to move

	//related script
	public BossStats bs;

	void Awake() {
		bs = GetComponent<BossStats> ();
	}
	
	public IEnumerator Hover() {
		while (bs.phase == 1) {
			//move left gradually faster until past the desired spot
			if (movingLeft) {
				transform.position = Vector3.Lerp (transform.position, leftAnchor, speed * Time.deltaTime);
				if (Vector3.Distance (transform.position, leftAnchor) < offset) {
					movingLeft = false;
					speed = 0.02f;
				}
				speed += 0.01f;
			} 
		//move right gradually faster until past the desired spot
		else if (!movingLeft) {
				transform.position = Vector3.Lerp (transform.position, rightAnchor, speed * Time.deltaTime);
				if (Vector3.Distance (transform.position, rightAnchor) < offset) {
					movingLeft = true;
					speed = 0.02f;
				}
				speed += 0.01f;
			}
			yield return new WaitForFixedUpdate ();
		}
		movingLeft = true;
		speed = 0f;
		StartCoroutine (PhaseTransition ());
	}

	//movement that happens when boss transitions into phase 2
	IEnumerator PhaseTransition() {
		//move left off screen
		while (movingLeft) {
			transform.position = Vector3.Lerp (transform.position, farLeftAnchor, speed * Time.deltaTime);
			if (Vector3.Distance (transform.position, farLeftAnchor) < offset) {
				transform.rotation = Quaternion.Euler (0, 90, 0);
				speed = 0.02f;
				movingLeft = false;
			}
			speed += 0.025f;
			yield return new WaitForFixedUpdate ();
		}
		//move right, back on screen, to the center of the screen (same vertical level)
		while (!movingLeft) {
			transform.position = Vector3.Lerp (transform.position, centerAnchor, speed * Time.deltaTime);
			if (Vector3.Distance (transform.position, centerAnchor) < offset) {
				StopCoroutine (PhaseTransition());
			}
			speed += 0.025f;
			yield return new WaitForFixedUpdate ();
		}
		bs.phase = 2;
	}


}
