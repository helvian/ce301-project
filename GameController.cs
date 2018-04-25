using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Main brain script controlling the pace of the game, holding variables that store
 * the player, all the enemies, all the enemy spawn locations...
 * Also responsible for managing user input not pertaining to the ship itself 
 */

public class GameController : MonoBehaviour {

	//the enemies, the player, and where they can spawn
	public GameObject[] hazards;
	public GameObject player;
	public GameObject[] spawnValues;

	//timing variables
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float largeWait;

	//wave counting to track when boss should spawn
	public float wavesSpawned;
	public int bossThreshold;

	//some text elements on screen 
	public Text healthText;
	private int score;

	//which pattern of enemies will spawn, and how many there are
	private int wavePattern;
	private int numPatterns = 5;

	//which type of large enemy will spawn, and how many there are
	private int largeEnemy;
	private int numLargeEnemies = 2;

	//flags depicting game states
	private bool boss1Spawned;
	public bool boss1Dead;
	private bool restart;

	//associated scripts
	public PlayerController pc;
	public TextController tc;
	public AudioSource music;

	//initialise variables and begin coroutines 
	void Start(){
		tc = GetComponent<TextController> ();
		restart = false;
		score = 0;
		healthText.text = "Health: " + pc.ps.health.ToString (); 
		tc.UpdateScore (score);
		music = GameObject.Find ("Music").GetComponent<AudioSource> ();
		StartCoroutine (SpawnWaves ());
		StartCoroutine (SpawnLarge ());
		StartCoroutine (SpawnBoss2 ());
	}

	//read for inputs every frame
	void Update() {
		//if the player is dead allow them to press S to reset the game
		if (restart) {
			if (Input.GetKeyDown (KeyCode.S)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
			}
		}
		//pause the game with P, unpause with P again
		if (Input.GetKeyDown (KeyCode.P) && Time.timeScale == 1) {
			Time.timeScale = 0;
		} else if (Input.GetKeyDown (KeyCode.P) && Time.timeScale == 0) {
			Time.timeScale = 1;
		}
	}

	//coroutine that controls the first boss and all of the normal enemy spawn patterns
	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds (startWait);
		wavePattern = Random.Range (1, numPatterns+1); //pick a random enemy pattern

		//before the first boss has spawned
		while (!boss1Spawned) {
			//check if boss should spawn
			if (wavesSpawned > bossThreshold) {

				//change music and spawn the first boss at an arbitrary location
				if (!boss1Spawned) { //failsafe
					music.Stop ();
					music.clip = Resources.Load ("Music/Darkling") as AudioClip;
					music.Play ();
					Instantiate (hazards [3], new Vector3 (99, 99, 10), Quaternion.Euler (0, 270, 0));
					boss1Spawned = true;
				}
			}	
			//main enemy spawning logic
			if (!boss1Spawned) { //failsafe
				/*
				 * For all of these cases in this switch statement the logic is similar to each other
				 * Set the timing variables as necessary, then spawn a chosen enemy in the hazards array
				 * as many times as depicted and in the position chosen
				 * Then wait before beginning again
				 */
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

						//spawn a powerup enemy instead
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

				//rechoose a new wave pattern and increment the counter
				wavePattern = Random.Range (0, numPatterns+1);
				wavesSpawned++;

				yield return new WaitForSeconds (waveWait);
			}

			//if the player is dead, stop the coroutine and allow a restart
			if (pc.ps.health <= 0) {
				restart = true;
				tc.ShowRestart ();
				break;
			}
		}
	}

	//coroutine responsible for spawning in larger enemies
	IEnumerator SpawnLarge(){
		yield return new WaitForSeconds (startWait);

		//before the first boss has spawned
		while (!boss1Spawned) {
			yield return new WaitForSeconds (largeWait);

			//choose a random large enemy to spawn
			largeEnemy = Random.Range (1, numLargeEnemies + 1);
			Vector3 spawnPosition;
			Quaternion spawnRotation;

			if (!boss1Spawned) {//failsafe
				
				//algorithm similar to SpawnWaves
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
			//stop if player is dead
			if (pc.ps.health <= 0) {
				break;
			}
		}
	}

	//coroutine that waits until the first boss is killed to spawn in the second boss
	IEnumerator SpawnBoss2(){
		while (!boss1Dead) {
			//game over check
			if (pc.ps.health <= 0) {
				restart = true;
				tc.ShowRestart ();
				break;
			}
			yield return new WaitForSeconds (10f); //delay the check for spawning
		}
		//spawn the boss
		Instantiate (hazards [7], new Vector3 (99, 99, 10), Quaternion.identity);
	}

}
