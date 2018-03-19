using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHurtByContact : MonoBehaviour {

	private Boss1Controller boss1Controller;
	private Boss2Controller boss2Controller;
	private PlayerController playerController;
	//public GameObject hitLight;
	//public Light flash;

	void Start () {
		boss1Controller = GetComponent<Boss1Controller> ();
		boss2Controller = GetComponent<Boss2Controller> ();
		playerController = GameObject.FindObjectOfType<PlayerController>();
		//hitLight = GameObject.FindGameObjectWithTag ("Hit Flash");
		//flash = hitLight.GetComponent<Light> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary") {
			return;
		}
		if (other.tag == "Player") {
			if (!playerController.ps.invincible) {
				playerController.TakeDamage ();
			}
		} else if (boss1Controller != null) {
			boss1Controller.TakeDamage (playerController.ps.damage);
			//StartCoroutine (HitFlash ());
			Destroy (other.gameObject);
		} else if (boss2Controller != null) {
			boss2Controller.TakeDamage (playerController.ps.damage);
			Destroy (other.gameObject);
		}
	}

	/*IEnumerator HitFlash(){
		if (!flash.enabled) {
			flash.enabled = true;
		}
		yield return 10;
		flash.enabled = false;
		yield break;
	}*/
}
