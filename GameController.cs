using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public GameObject player;
	public GameObject[] spawnValues;
	public int hazardCount;

	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float largeWait;
	public float wavesSpawned;
	public int bossThreshold;

	public Text scoreText;
	public Text healthText;
	public Text gameOverText;
	private int score;

	private int wavePattern;
	private int numPatterns = 5;
	private int largeEnemy;
	private int numLargeEnemies = 2;

	public int lives;
	private bool bossSpawned;
	private bool restart;

	public PlayerController pc;
	public TextController tc;

	void Start(){
		tc = GetComponent<TextController> ();
		restart = false;
		score = 0;
		healthText.text = "Health: " + pc.ps.health.ToString ();
		tc.UpdateScore (score);
		StartCoroutine (SpawnWaves ());
		StartCoroutine (SpawnLarge ());
	}

	void Update() {
		if (restart) {
			if (Input.GetKeyDown (KeyCode.S)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds (startWait);
		wavePattern = Random.Range (4, 5);
		while (!bossSpawned) {
			//check if boss should spawn
			if (wavesSpawned > bossThreshold) {
				if (!bossSpawned) {
					Instantiate (hazards [3], new Vector3 (99, 99, 10), Quaternion.Euler (0, 270, 0));
					bossSpawned = true;
				}
			}	
			if (!bossSpawned) {
				switch (wavePattern) {
				case (1):
					spawnWait = 0.5f;
					for (int i = 0; i < 10; i++) {
						spawnWait = 0.5f;
						waveWait = 4f;
						Vector3 spawnPosition = new Vector3 (Random.Range (spawnValues [0].transform.position.x, spawnValues [1].transform.position.x),
							                        spawnValues [0].transform.position.y, spawnValues [0].transform.position.z);
						Quaternion spawnRotation = Quaternion.identity * Quaternion.Euler (0, 180, 0);
						Instantiate (hazards [2], spawnPosition, spawnRotation);
						yield return new WaitForSeconds (spawnWait);
					}
					break;
				case (2):
					for (int i = 0; i < 8; i++) {
						spawnWait = 0.5f;
						waveWait = 4f;
						Vector3 spawnPosition = new Vector3 (spawnValues [1].transform.position.x - 2 * i, spawnValues [1].transform.position.y,
							                        spawnValues [1].transform.position.z);
						Quaternion spawnRotation = Quaternion.identity * Quaternion.Euler (0, 180, 0);
						if (i == 7) {
							Instantiate (hazards [5], spawnPosition, spawnRotation);
						}
						Instantiate (hazards [0], spawnPosition, spawnRotation);
						yield return new WaitForSeconds (spawnWait);
					}
					break;
				case (3):
					for (int i = 0; i < 8; i++) {
						spawnWait = 0.5f;
						waveWait = 4f;
						Vector3 spawnPosition = new Vector3 (spawnValues [0].transform.position.x + 2 * i, spawnValues [0].transform.position.y, 
							                        spawnValues [0].transform.position.z);
						Quaternion spawnRotation = Quaternion.identity * Quaternion.Euler (0, 180, 0);
						if (i == 7) {
							Instantiate (hazards [5], spawnPosition, spawnRotation);
						}
						Instantiate (hazards [0], spawnPosition, spawnRotation);
						yield return new WaitForSeconds (spawnWait);
					}
					break;
				case (4):
					for (int i = 0; i < 4; i++) {
						spawnWait = 0.25f;
						waveWait = 2f;
						Vector3 spawnPosition = new Vector3 (spawnValues [2].transform.position.x, spawnValues [2].transform.position.y, 
							                        spawnValues [2].transform.position.z + 1.25f * i);
						Quaternion spawnRotation = (Quaternion.identity * Quaternion.Euler (0, 90, 0));
						Instantiate (hazards [1], spawnPosition, spawnRotation);
						yield return new WaitForSeconds (spawnWait);
					}
					break;
				case (5):
					for (int i = 0; i < 4; i++) {
						spawnWait = 0.25f;
						waveWait = 2f;
						Vector3 spawnPosition = new Vector3 (spawnValues [3].transform.position.x, spawnValues [3].transform.position.y, 
							                        spawnValues [3].transform.position.z + 1.25f * i);
						Quaternion spawnRotation = (Quaternion.identity * Quaternion.Euler (0, 270, 0));
						Instantiate (hazards [1], spawnPosition, spawnRotation);
						yield return new WaitForSeconds (spawnWait);
					}
					break;
				}
			
				wavePattern = Random.Range (2, 3);
				wavesSpawned++;

				yield return new WaitForSeconds (waveWait);
			}

			if (pc.ps.health <= 0) {
				restart = true;
				tc.ShowRestart ();
				break;
			}
		}
	}

	IEnumerator SpawnLarge(){
		yield return new WaitForSeconds (startWait);
		while (!bossSpawned) {
			yield return new WaitForSeconds (largeWait);
			largeEnemy = Random.Range (1, numLargeEnemies + 1);
			Vector3 spawnPosition;
			Quaternion spawnRotation;
			if (!bossSpawned) {
				switch (largeEnemy) {
				case (1):
					spawnPosition = new Vector3 (100, 0, 100);
					spawnRotation = (Quaternion.identity * Quaternion.Euler (0, 0, 0));
					Instantiate (hazards [4], spawnPosition, spawnRotation);
					break;
				case (2):
					spawnPosition = spawnValues [0].transform.position;
					spawnPosition += new Vector3 (Random.Range(-5, 5), 0, 0);
					spawnRotation = (Quaternion.identity * Quaternion.Euler (0, 0, 0));
					Instantiate (hazards [6], spawnPosition, spawnRotation);

					break;
				}
			}
			if (pc.ps.health <= 0) {
				break;
			}
		}
	}
}
