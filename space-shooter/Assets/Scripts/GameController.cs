using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValue;
	public int hazardCount;
	public float spawnWait;
	public float stratWait;
	public float waveWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;



	private int score;
	private bool gameOver;
	private bool restart;

	void Start () {
		score = 0;
		updateScore ();
		StartCoroutine (SpawnWaves());
		restartText.text = "";
		gameOverText.text = "";
		gameOver = false;
		restart = false;
	}

	void Update () {
		if (restart) {
			if(Input.GetKeyDown (KeyCode.R)){
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds(spawnWait);
		while (true) {
			for (int i = 0; i<hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

			if(gameOver){
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	void updateScore(){
		scoreText.text = "Score : " + score;
	}

	public void playerDead () {
		gameOverText.text = "Game Over";
		gameOver = true;
	}

	public void addScore(int newScoreValue){
		score += newScoreValue;
		updateScore ();
	}
}
