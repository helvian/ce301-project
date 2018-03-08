using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {

	MeshRenderer mr;

	public float speed;

	void Start() {
		mr = GetComponent<MeshRenderer> ();
	}
	void Update () {
		Material m = mr.material;
		Vector2 offset = m.mainTextureOffset;
		offset.y += Time.deltaTime * speed;
		m.mainTextureOffset = offset;
	}
}
