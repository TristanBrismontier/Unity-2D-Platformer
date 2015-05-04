using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public GameObject enemy;

	public Transform startPosition;

	private int life;
	private int startLife;
	private GameObject[] hearts; 

	void Awake () {
		if (instance == null){
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad(gameObject);
	}
	void Start () {
		InitGame ();
	}

	void InitGame ()
	{
		startLife = hearts.Length;
		life = startLife;
		for(int i=0;i< hearts.Length; i++){
			 hearts[i].SetActive(true);
		}
		float randomEnn = Random.Range (4, 4);
		for (int i = 0; i < randomEnn; i++) {
			AddEnemy ();
		}
	}

	public void AddEnemy(){
		float x = (float)(Random.Range(20,200)/10);
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		float delta = x  - go.transform.position.x;
		if( Mathf.Abs(delta) < 2.1f ){
			Debug.Log ("Pos Player : " + go.transform.position.x + "Fix Bitch : " + x + " delta : " +delta + " Fix : "+ x+((delta<=0)?-1:1));
			x=x+((delta<=0)?-1:1);
		}
		Instantiate(enemy, new Vector3( x, 2.1f, 0), Quaternion.identity);
	}

	public bool TakeDamage(int damage){
		if(life>0){
			hearts[life-1].SetActive(false);
			life--;
			return true;
		}else{
			Restart();
			return false;
		}

	}

	public void Restart()
	{
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		go.transform.position = startPosition.position;
		InitGame ();
	}

	public void SetHearts(GameObject[] _hearts){
		hearts =  _hearts;
	} 
}
