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
	void Start () {
		float randomEnn = Random.Range(4,4);
		for(int i=0;i<randomEnn;i++ ){
			AddEnemy();
		}
	}

	public void AddEnemy(){
		Instantiate(enemy, new Vector3(Random.Range(2,15), 0.5f, 0), Quaternion.identity);
	}

	public bool TakeDamage(int damage){
		//TODO 
		return true;
	}
}
