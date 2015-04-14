using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public GameObject enemy;

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
	// Use this for initialization
	void Start () {
		AddEnemy();
	}

	public void AddEnemy(){
		Instantiate(enemy, new Vector3(Random.Range(0,12), 0.5f, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
