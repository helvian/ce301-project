﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	private int score;
	private int health;

	public Text scoreText;
	public Text healthText;
	public Text gameOverText;
	public Text restartText;

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
}