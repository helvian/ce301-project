using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * Script that controls various text elements on the screen
 */

public class TextController : MonoBehaviour {

	//copies of other variables in other scripts
	private int score;
	private int health;
	private string weapon = "Gatling";

	//text variables on the screen
	public Text scoreText;
	public Text healthText;
	public Text gameOverText;
	public Text restartText;
	public Text weaponText;

	/*
	 * Various update functions that manipulate text on screen,
	 * called at appropriate times when variables are changed in their own scripts
	 */

	public void UpdateScore(int s){
		this.score += s;
		scoreText.text = "Score: " + score;
	}

	public void UpdateHealth(int h) {
		this.health = h;
		healthText.text = "Health: " + health;
	}

	public void ShowGameOver() {
		gameOverText.enabled = true;
	}

	public void ShowRestart() {
		restartText.enabled = true;
	}

	public void HideGameOverAndRestart() {
		gameOverText.enabled = false;
		restartText.enabled = false;
	}

	public void UpdateWeapon(string w) {
		this.weapon = w;
		weaponText.text = "Weapon: " + weapon;
	}
}
