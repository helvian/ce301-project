using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Brain script for the powerup system, controls the UI of the powerup bar also
 */

public class PowerupController : MonoBehaviour {

	public int powerupsHeld; //how many powerups the player has stored
	public Image[] powerupBars; //the images of each powerup bar, in order
	public Image[] powerupBarTexts; //the images of text on each bar, in order
	public int optionsSpawned; //how many Options are in existence

	public GameObject option; //the object to be spawned as an Option
	private GameObject player; //the player object

	//associated scripts
	private PlayerStats ps;
	private TextController tc;

	//initialisation
	void Start () {
		powerupsHeld = 0;
		player = GameObject.FindGameObjectWithTag ("Player");
		ps = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats> ();
		tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();
	}

	//called when the player collides with a powerup
	public void IncrementMeter() {
		//do not allow more than 4 powerups
		if (powerupsHeld < 4) { 
			powerupsHeld++;
		}
		//from 0 to 1 powerups, make the first bar blue
		if (powerupsHeld == 1) {
			powerupBars [powerupsHeld - 1].color = new Color (0, 255, 255, 255);
		} 
		//otherwise make the next bar blue and the last bar white
		else {
			powerupBars [powerupsHeld - 2].color = new Color (255, 255, 255, 255);
			powerupBars [powerupsHeld - 1].color = new Color (0, 255, 255, 255);
		}
	}

	//called when the player presses the cash-in button (Spacebar by default)
	public void CashInPowerups() {
		//depending on how many powerups they have
		switch (powerupsHeld) {
		//failsafe, should never be called
		case 0:
			break;
		//one powerup: increase their speed up to a maximum
		case 1:
			if (ps.speed < ps.maxSpeed) {
				ps.speed += 2;
				Mathf.Clamp (ps.speed, 12, ps.maxSpeed);

				//hide the text on the bar if the max is reached
				if (ps.speed == ps.maxSpeed) {
					powerupBarTexts [powerupsHeld - 1].enabled = false;
				}

				//reset the colour of the blue powerup bar, and the held powerups
				powerupBars [powerupsHeld - 1].color = new Color (255, 255, 255, 255);
				powerupsHeld = 0;
			}
			break;
		//two powerups: increase their health
		case 2:
			ps.health++;
			tc.UpdateHealth (ps.health);

			//reset the colour of the blue powerup bar, and the held powerups
			powerupBars [powerupsHeld - 1].color = new Color (255, 255, 255, 255);
			powerupsHeld = 0;
			break;
		//three powerups: increase the number of shots fired per fire command, up to a maximum
		//also increase the number of homing marks that can be applied and how fast they are applied
		case 3:
			if (ps.numShots < ps.maxShots) {
				ps.numShots++;
				ps.lockOnMax += 5;
				ps.lockOnRate -= 0.125f;
				Mathf.Clamp (ps.numShots, 2, ps.maxShots);

				//hide the text on the bar if the max is reached
				if (ps.numShots == ps.maxShots) {
					powerupBarTexts [powerupsHeld - 1].enabled = false;
				}

				//reset the colour of the blue powerup bar, and the held powerups
				powerupBars [powerupsHeld - 1].color = new Color (255, 255, 255, 255);
				powerupsHeld = 0;
			}
			break;

		//four powerups: spawn an Option on the player
		case 4:
			optionsSpawned++;
			Instantiate (option, player.transform.position, Quaternion.Euler (90, 0, 0));

			//reset the colour of the blue powerup bar, and the held powerups
			powerupBars [powerupsHeld - 1].color = new Color (255, 255, 255, 255);
			powerupsHeld = 0;
			break;
		}
	}
}
