using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public BoardManager boardScript;

	public float moveRate = 1F;
	private float nextMove = 0.0F;

	private float xDir = 0.05f;
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
		if ( Time.time > nextMove) {
			nextMove = Time.time + moveRate;
			float x = changeDirection == true ? 0:xDir;
			float y= changeDirection == true? yDir:0;
			if(changeDirection){
				xDir = xDir *-1.25f;
				yDir = yDir * 1.25f;
				moveRate = moveRate/1.2f;
				Debug.Log ("moveRate");
				changeDirection = false;
			}
			foreach(Ennemy ennemy in ennemies){
				ennemy.Move(x,y);
			}
		}
		Debug.Log("FinUpdate");
	}

	public void EnnemyContactBorder(){
		changeDirection = true;
		Debug.Log("ChangeDirection");
	}
}
