using UnityEngine;
using System.Collections;

public class ScanBox : MonoBehaviour {

	//public Collider box;
	public SideEnemyController sec;

	void Start() {
		//box = GetComponent<BoxCollider> ();
		sec = GetComponentInParent<SideEnemyController> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Lockon") {
			return;
		}
		sec.Fire ();
	}


}
