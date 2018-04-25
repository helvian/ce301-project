using UnityEngine;
using System.Collections;

/*
 * The brains of enemies that use the Unity animation module to move them.
 * 
 * 
 */
public class AnimatedEnemyController : MonoBehaviour {

	public ParticleSystem shot; //the weapon attached to this enemy

	public Animator an; //Unity's animator attached to the enemy
	public bool shootNow = false; //flag inside the animator to tell the ship to shoot
	private bool dead = false; //if the ship has been killed

	public GameObject spray; //the effect that plays on the ship when it is shot at

	//other attached scripts
	private EnemyStats es; 
	private EffectOnDeath eod;
	public TextController tc;

	//initialise scripts into variables
	void Start () {
		es = GetComponent<EnemyStats> ();
		eod = GetComponent<EffectOnDeath> ();
		an = GetComponent<Animator> ();
		shot = GetComponentInChildren<ParticleSystem> ();
		tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();
	}
		
	void Update() {
		//when the right animation frame has passed, fire the weapon
		//don't fire more than once
		if (shootNow && !shot.isPlaying) {
			shot.Play ();
		}
	}

	//called when this ship is hit
	public void TakeDamage(float damage) {
		es.health -= damage;
		Instantiate (spray, transform.position, Quaternion.identity);
		if (es.health <= 0) {
			if (!dead) {
				Death ();
			}
		}
	}

	//called when this ship reaches zero health
	void Death() {
		eod.SpawnEffect ();
		dead = true;
		tc.UpdateScore (es.score);
		shot.Stop ();

		//re-parenting, re-sizing, and timing the destruction of the weapons attached
		Vector3 tempVec = shot.transform.lossyScale;
		tempVec.x = 1 / shot.transform.parent.lossyScale.x;
		tempVec.y = 1 /shot.transform.parent.lossyScale.y;
		tempVec.z = 1 /shot.transform.parent.lossyScale.z;
		shot.transform.localScale = tempVec;

		shot.transform.parent = GameObject.Find("Dead Particles").transform;

		Destroy (shot.gameObject, 10.0f);
		Destroy (gameObject);
	}
}

