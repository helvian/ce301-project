using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupController : MonoBehaviour {

	public int powerupsHeld; //how many powerups the player has stored
	public Image[] powerupBars; //the images of each powerup bar, in order
	public Image[] powerupBarTexts; //the images of text on each bar, in order
	public int optionsSpawned;

	public GameObject option;
	private GameObject player;

	private PlayerStats ps;
	private TextController tc;

	void Start () {
		powerupsHeld = 0;
		player = GameObject.FindGameObjectWithTag ("Player");
		ps = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats> ();
		tc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<TextController> ();
	}
	
	public void IncrementMeter() {
		if (powerupsHeld < 5) { //do not allow more than 5 powerups
			powerupsHeld++;
		}
		if (powerupsHeld == 1) {
			powerupBars [powerupsHeld - 1].color = new Color (0, 255, 255, 255);
		} else {
			powerupBars [powerupsHeld - 2].color = new Color (255, 255, 255, 255);
			powerupBars [powerupsHeld - 1].color = new Color (0, 255, 255, 255);
		}
	}

	public void CashInPowerups() {
		switch (powerupsHeld) {
		case 0:
			break;
		case 1:
			if (ps.speed < ps.maxSpeed) {
				ps.speed += 2;
				Mathf.Clamp (ps.speed, 12, ps.maxSpeed);
				if (ps.speed == ps.maxSpeed) {
					powerupBarTexts [powerupsHeld - 1].enabled = false;
				}
				powerupBars [powerupsHeld - 1].color = new Color (255, 255, 255, 255);
				powerupsHeld = 0;
			}
			break;
		case 2:
			ps.health++;
			tc.UpdateHealth (ps.health);
			powerupBars [powerupsHeld - 1].color = new Color (255, 255, 255, 255);
			powerupsHeld = 0;
			break;
		case 3:
			if (ps.numShots < ps.maxShots) {
				ps.numShots++;
				ps.lockOnMax += 5;
				ps.lockOnRate -= 0.125f;
				Mathf.Clamp (ps.numShots, 2, ps.maxShots);
				if (ps.numShots == ps.maxShots) {
					powerupBarTexts [powerupsHeld - 1].enabled = false;
				}
				powerupBars [powerupsHeld - 1].color = new Color (255, 255, 255, 255);
				powerupsHeld = 0;
			}
			break;
		case 4:
			optionsSpawned++;
			Instantiate (option, player.transform.position, Quaternion.Euler (90, 0, 0));
			powerupBars [powerupsHeld - 1].color = new Color (255, 255, 255, 255);
			powerupsHeld = 0;
			break;
		case 5:
			break;
		}
	}
}
