using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public BoardManager boardScript;
	public Text scoreText;
	public Text liveText;

	public float moveRatepublic = 1f;
	public int lives;

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
		score = 0;
		addEnnemyDestroyScore(0);
		DontDestroyOnLoad(gameObject);
		boardScript = GetComponent<BoardManager> ();
		InitEnnemySystem();
		liveText.text = lives+" Live";
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
		CheckEnnemyAlive();
		if ( Time.time > nextMove) {
			nextMove = Time.time + moveRate;
			float x = changeDirection == true ? 0:xDir;
			float y= changeDirection == true? yDir:0;
			if(changeDirection){
				xDir = xDir *-1f;
				moveRate = moveRate/1.15f;
				Debug.Log ("moveRate : "+ moveRate);
				changeDirection = false;
			}
			foreach(Ennemy ennemy in ennemies){
				ennemy.Move(x,y);
			}
		}
	}

	void CheckEnnemyAlive(){
		foreach(Ennemy ennemy in ennemies){
			if(ennemy.isActiveAndEnabled){
				return;
			}
		}
		InitEnnemySystem ();
	}

	public void addEnnemyDestroyScore(int _score){
		score += _score;
		scoreText.text = " Score: "+score;
	}

	public void EnnemyContactBorder(){
		changeDirection = true;
	}

	public void PlayerLooseLive(){
		lives--;
		liveText.text = lives+" Live";
	}
}
