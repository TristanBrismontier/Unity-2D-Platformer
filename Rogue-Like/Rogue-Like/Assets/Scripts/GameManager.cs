using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public BoardMana boardScript; 
	public int playerFoodPoints = 100;

	[HideInInspector]public bool playerTurn = true;

	private int level = 3;
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
		boardScript = GetComponent<BoardMana> ();
		InitGame ();
	}

	void InitGame() {
		boardScript.SetupScene (level);
	}

	public void GameOver(){
		enabled = false;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
