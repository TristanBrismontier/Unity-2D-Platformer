using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public BoardManager boardScript;

	public bool canFire = true;

	public float moveRate = 1F;
	private float nextMove = 0.0F;

	private float xDir = 0.1f;
	private float yDir = -0.1f;

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
		ennemies = new List<Ennemy> ();
		boardScript = GetComponent<BoardManager> ();
		boardScript.BoardSetup();
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
		Debug.Log("FinUpdate");
	}

	void CheckEnnemyAlive(){
		foreach(Ennemy ennemy in ennemies){
			if(ennemy.isActiveAndEnabled){
				return;
			}
		}
		  moveRate = 1F;
		  nextMove = 0.0F;
		  xDir = 0.1f;
		  yDir = -0.1f;
		ennemies = new List<Ennemy> ();
		boardScript.BoardSetup();
	}

	public void addEnnemyDestroyScore(int _score){

	}

	public void EnnemyContactBorder(){
		changeDirection = true;
		Debug.Log("ChangeDirection");
	}
}
