using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
	public float speed;
	public float maxSpeed;
	public float tilt;

	public int numShots;
	public int maxShots;
	public float fireRate;
	public float nextFire;
	public float homingCD;
	public float nextHoming;
	public float damage;

	public int health;
	public bool invincible;
}
