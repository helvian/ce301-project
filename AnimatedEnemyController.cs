using UnityEngine;
using System.Collections;

public class AnimatedEnemyController : MonoBehaviour {

	public ParticleSystem shot;
	//public GameObject shot;
	//public Transform shotSpawn;

	public Animator an;
	public bool shootNow = false;
	private bool dead = false;

	public GameObject spray;

	private EnemyStats es;
	public TextController tc;

	void Start () {
		es = GetComponent<EnemyStats> ();
		an = GetComponent<Animator> ();
		shot = GetComponentInChildren<ParticleSystem> ();
		tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();
	}

	void Update() {
		if (shootNow && !shot.isPlaying) {
			shot.Play ();
		}
	}

	public void TakeDamage(float damage) {
		es.health -= damage;
		Instantiate (spray, transform.position, Quaternion.identity);
		if (es.health <= 0) {
			if (!dead) {
				Death ();
			}
		}
	}

	void Death() {
		dead = true;
		tc.UpdateScore (es.score);
		shot.Stop ();
		Vector3 tempVec = shot.transform.lossyScale;
		Debug.Log (shot.transform.lossyScale);
		tempVec.x = 1 / shot.transform.parent.lossyScale.x;
		tempVec.y = 1 /shot.transform.parent.lossyScale.y;
		tempVec.z = 1 /shot.transform.parent.lossyScale.z;
		shot.transform.localScale = tempVec;
		shot.transform.parent = GameObject.Find("Dead Particles").transform;
		Destroy (shot, 10.0f);
		Destroy (gameObject);
	}
}

