using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHurtByContact : MonoBehaviour {

	private Boss1Controller bossController;
	private PlayerController playerController;
	public GameObject hitLight;
	public Light flash;

	void Start () {
		bossController = GetComponent<Boss1Controller> ();
		playerController = GameObject.FindObjectOfType<PlayerController>();
		hitLight = GameObject.FindGameObjectWithTag ("Hit Flash");
		flash = hitLight.GetComponent<Light> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary") {
			return;
		}
		if (other.tag == "Player") {
			if (!playerController.ps.invincible) {
				playerController.TakeDamage ();
			}
		} else {
			bossController.TakeDamage (playerController.ps.damage);
			StartCoroutine (HitFlash ());
			Destroy (other.gameObject);
		}
	}

	IEnumerator HitFlash(){
		if (!flash.enabled) {
			flash.enabled = true;
		}
		yield return 10;
		flash.enabled = false;
		yield break;
	}
}
