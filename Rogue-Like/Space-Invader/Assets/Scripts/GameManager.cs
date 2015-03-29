using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public BoardManager boardScript;
	public Text scoreText;
	public Image[] livesImg;
	public GameObject gameOverUI;

	public float moveRatepublic = 1f;
	public int lives;

	public bool gameOver = false;
	public float timeToRestart = 1F;
	private float nextRestart = 0.0F;

	private int score;
	private float moveRate, nextMove;
	private float xDir ,yDir;
	private bool changeDirection; 
	private List<Ennemy> ennemies;

	// Use this for initialization
	void Awake () {
		if (instance == null){
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
		boardScript = GetComponent<BoardManager> ();
		SartGame ();
		InitEnnemySystem ();
	}

	void SartGame ()
	{
		lives = 3;
		gameOver = false;
		score = 0;
		scoreText.text = "Score: " + score;

		gameOverUI.SetActive (false);
		livesImg [0].enabled = true;
		livesImg [1].enabled = true;
		livesImg [2].enabled = true;
	}

	void InitEnnemySystem ()
	{
		moveRate = moveRatepublic;
		nextMove = 0.0F;
		xDir = 0.1f;
		yDir = -0.1f;
		ennemies = new List<Ennemy> ();
		boardScript.BoardSetup ();
	}

	public void addEnemyToList(Ennemy script){
		ennemies.Add (script);
	}

	void Update() {
		if(gameOver && Input.anyKey && Time.time > nextRestart){
			SartGame ();
		}
		if(!gameOver){
			CheckEnnemyAlive();
			if ( Time.time > nextMove) {
				nextMove = Time.time + moveRate;
				float x = changeDirection == true ? 0:xDir;
				float y= changeDirection == true? yDir:0;
				if(changeDirection){
					xDir = xDir *-1f;
					moveRate = moveRate/1.15f;
					changeDirection = false;
				}
				foreach(Ennemy ennemy in ennemies){
					ennemy.Move(x,y);
				}
			}
		}

	}

	void CheckEnnemyAlive(){
		if(ennemies.Count >0)
			return;
		InitEnnemySystem ();
	}

	public void addEnnemyDestroyScore(int _score, Ennemy script){
		ennemies.Remove (script);
		score += _score;
		scoreText.text = " Score: "+score;
	}

	public void EnnemyContactBorder(){
		changeDirection = true;
	}

	public void GameOver(){
		foreach(Ennemy enn in ennemies){
			Destroy(enn.gameObject);
		}
		ennemies.Clear();
		gameOver = true;
		gameOverUI.SetActive(true);
		InitEnnemySystem();
		nextRestart = Time.time + timeToRestart;
	}

	public void PlayerLooseLive(){
		if(lives == 0){
			GameOver();
			return;
		}
		lives--;
		livesImg[lives].enabled =false;
	}
}
