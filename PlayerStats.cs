using UnityEngine;
using System.Collections;

/*
 * Container script for the player and their stats
 */

public class PlayerStats : MonoBehaviour {

	//how fast they move, how fast they are allowed to move with powerups
	public float speed;
	public float maxSpeed;

	//how much the ship tilts while moving
	public float tilt;

	//how many shots fire upon one fire command, how many are allowed to fire with powerups
	public int numShots;
	public int maxShots;

	//how much time must pass for a shot to fire after another
	public float fireRate;
	public float nextFire;

	//the lockout time after using the homing weapon
	public float homingCD;
	public float nextHoming;

	//the amount of lockon marks that can be applied across all enemies
	public int lockOnMax;

	//how fast lockon marks are applied
	public float lockOnRate;

	//how much damage all attacks do to enemies
	public float damage;

	//how many hits the player can take, and if they take damage at all
	public int health;
	public bool invincible;
}
