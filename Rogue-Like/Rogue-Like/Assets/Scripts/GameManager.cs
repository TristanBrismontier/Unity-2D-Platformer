using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public float levelStartDelay = 2f;
	public float turnDelay = .1f;
	public static GameManager instance = null;
	public BoardMana boardScript; 
	public int playerFoodPoints = 100;

	[HideInInspector]public bool playerTurn = true;

	private Text levelText;
	private GameObject levelImage;
	private int level = 1;
	private List<Ennemy> ennemies;
	private bool enemiesMoving;
	private bool doingStetup;


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

	private void OnLevelWasLoaded (int Index){
		level ++;
		InitGame ();

	}

	void InitGame() {
		doingStetup = true;
		levelImage = GameObject.Find ("LevelImage");
		levelText = GameObject.Find ("LevelText").GetComponent<Text> ();
		levelText.text = "Day " + level;
		levelImage.SetActive (true);
		Invoke ("HideLevelImage", levelStartDelay);

		ennemies.Clear ();
		boardScript.SetupScene (level);
	}

	private void HideLevelImage (){
		Debug.Log ("Hide");
		levelImage.SetActive (false);
		doingStetup = false;

	}

	public void GameOver(){
		levelText.text = "After " + level + " days, you starved.";
		levelImage.SetActive (true);
		enabled = false;
	}
	// Update is called once per frame
	void Update () {
		if (playerTurn || enemiesMoving || doingStetup) {
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
