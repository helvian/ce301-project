using UnityEngine;
using System.Collections;

/*
 * Script that controls the behaviour of the Option's weaponry
 */

public class OptionController : MonoBehaviour
{
	public GameObject shot; //the projectile it fires
	public Transform shotSpawn; //where the projectile appears

	//associated stat script
	public OptionStats os;

	//initialisation
	void Start ()
	{
		os = GetComponent<OptionStats> ();
	}

	//if the player is firing their weapons, fire the Option's weapons
	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > os.nextFire) {
			os.nextFire = Time.time + os.fireRate;
			Instantiate (shot, shotSpawn.position, Quaternion.identity);
		}
	}

}
