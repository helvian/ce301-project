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
	public float wavesSpawned;
	public int bossThreshold;

	public Text scoreText;
	public Text healthText;
	public Text gameOverText;
	private int score;

	public int wavePattern;
	private int numPatterns = 4;

	public int lives;
	private bool bossSpawned;
	private bool gameOver;
	private bool restart;
	private bool stageComplete;

	public PlayerController pc;
	public TextController tc;

	void Start(){
		tc = GetComponent<TextController> ();
		gameOver = false;
		restart = false;
		stageComplete = false;
		score = 0;
		healthText.text = "Health: " + pc.ps.health.ToString ();
		tc.UpdateScore (score);
		StartCoroutine (SpawnWaves ());
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
		wavePattern = Random.Range (1, numPatterns+1);
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
			
				wavePattern = Random.Range (1, numPatterns + 1);
				wavesSpawned++;

				//spawning powerup carriers
				if (bossThreshold % wavesSpawned >= 2) {
					Vector3 spawnPosition = new Vector3 (100, 0, 100);
					Quaternion spawnRotation = (Quaternion.identity * Quaternion.Euler (0, 0, 0));
					Instantiate (hazards [4], spawnPosition, spawnRotation);
				}
				yield return new WaitForSeconds (waveWait);
			}

			if (pc.ps.health <= 0) {
				restart = true;
				tc.ShowRestart ();
				break;
			}
		}
	}
}
