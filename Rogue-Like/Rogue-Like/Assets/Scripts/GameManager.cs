using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public float turnDelay = .1f;
	public static GameManager instance = null;
	public BoardMana boardScript; 
	public int playerFoodPoints = 100;

	[HideInInspector]public bool playerTurn = true;

	private int level = 3;
	private List<Ennemy> ennemies;
	private bool enemiesMoving;


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
		boardScript = GetComponent<BoardMana> ();
		InitGame ();
	}

	void InitGame() {
		ennemies.Clear ();
		boardScript.SetupScene (level);
	}

	public void GameOver(){
		enabled = false;
	}
	// Update is called once per frame
	void Update () {
		if (playerTurn || enemiesMoving) {
			return;
		}

		StartCoroutine (MoveEnemis ());
	}

	public void addEnemyToList(Ennemy script){
		ennemies.Add (script);
	}

	IEnumerator MoveEnemis(){
		enemiesMoving = true;
		yield return new WaitForSeconds(turnDelay);
		if (ennemies.Count == 0) {
			yield return new WaitForSeconds(turnDelay);
		}

		for (int i = 0; i<ennemies.Count; i++) {
			ennemies[i].MoveEnnemy();
			yield return new WaitForSeconds(ennemies[i].moveTime);
		}
		playerTurn = true;
			enemiesMoving = false;
	}
}
