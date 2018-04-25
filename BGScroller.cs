using UnityEngine;
using System.Collections;

/*
 * Basic script that scrolls the background of the game constantly
 */

public class BGScroller : MonoBehaviour {

	MeshRenderer mr; //the background

	public float speed; //the speed of scrolling

	void Start() {
		mr = GetComponent<MeshRenderer> ();
	}

	//every frame, move the image projected onto the background downward
	void Update () {
		Material m = mr.material;
		Vector2 offset = m.mainTextureOffset;
		offset.y += Time.deltaTime * speed;
		m.mainTextureOffset = offset;
	}
}
