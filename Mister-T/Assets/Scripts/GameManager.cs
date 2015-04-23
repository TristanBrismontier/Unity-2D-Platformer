using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public GameObject enemy;
	public GameObject[] hearts; 
	public Transform startPosition;

	private int life;
	private int startLife;

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
		Instantiate(enemy, new Vector3((float)(Random.Range(20,200)/10), 2.1f, 0), Quaternion.identity);
	}

	public bool TakeDamage(int damage){
		if(life>1){
			hearts[life-1].SetActive()
		}
		return true;
	}

	public void Restart()
	{
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		transform.position = startPosition.position;
		InitGame ();
	}
}
