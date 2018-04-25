using UnityEngine;
using System.Collections;

/*
 * Container for various stats belonging to bosses.
 */
public class BossStats : MonoBehaviour {

	//deprecated
	public float fireRate;
	public float nextFire;

	public float maxHealth; //the most health the boss can have, used for slider bar
	public float health; //the health the boss currently has

	public int phase; //the current phase the boss is in

}
