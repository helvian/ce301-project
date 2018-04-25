using UnityEngine;
using System.Collections;

/*
 * Brain script that controls the state of sideward flying enemies
 */

public class SideEnemyController : MonoBehaviour {
	private Rigidbody rb; //the physics body attached

	public ParticleSystem shot; //the weapon attached

	private bool dead; //if the enemy has been killed

	public GameObject spray; //the effect played when shot by the player

	//associated scripts
	private EnemyStats es;
	private EffectOnDeath eod;
	public TextController tc;
	public ScanBox sb;

	//initialisation
	void Start () {
		es = GetComponent<EnemyStats> ();
		eod = GetComponent<EffectOnDeath> ();
		rb = GetComponent<Rigidbody> ();
		sb = GetComponent<ScanBox> ();
		tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();

		//set velocity based on inputted speed
		rb.velocity = transform.forward * es.speed;
	}

	//function for the Scan Box to access when the conditions are met to fire
	public void Fire() {
		shot.Play ();
	}

	//called when the ship is hit by the player
	public void TakeDamage(float damage) {
		es.health -= damage;
		Instantiate (spray, transform.position, Quaternion.identity);
		if (es.health <= 0) {

			//can only be called once (because two of these functions can be called at once due to
			//how the bullets are fired and make contact)
			if (!dead) {
				Death ();
			}
		}
	}

	//played when ship reaches zero health
	void Death() {
		eod.SpawnEffect ();
		dead = true;
		tc.UpdateScore (es.score);
		shot.Stop ();

		//re-parenting, re-sizing, and timing the destruction of the weapons attached
		Vector3 tempVec = shot.transform.lossyScale;
		Debug.Log (shot.transform.lossyScale);
		tempVec.x = 1 / shot.transform.parent.lossyScale.x;
		tempVec.y = 1 /shot.transform.parent.lossyScale.y;
		tempVec.z = 1 /shot.transform.parent.lossyScale.z;
		shot.transform.localScale = tempVec;

		shot.transform.parent = GameObject.Find("Dead Particles").transform;

		Destroy (shot);
		Destroy (shot.gameObject, 10.0f);
		Destroy (gameObject);
	}
}

