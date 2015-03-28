using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {
	
	public int columns = 10;
	public int rows = 5;
	
	private Transform boardHolder;
	public GameObject[] ennemyTiles;
	
	public void BoardSetup(){
		boardHolder = new GameObject ("Board").transform;
		GameObject toInstantiate = ennemyTiles[0];
		GameObject instance = Instantiate(toInstantiate,new Vector3(0f,1f,0f),Quaternion.identity) as GameObject;
		instance.transform.SetParent(boardHolder);
	}
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
