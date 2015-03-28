using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {
	
	public int columns = 10;
	public int rows = 5;
	
	private Transform boardHolder;
	public GameObject[] ennemyTiles;
	
	public void BoardSetup(){
		boardHolder = new GameObject ("Board").transform;
		for (int x=0; x<columns; x++) {
			for(int y=0; y<rows; y++){
				int indexEnnemy = GetEnnemyIndex(y);
				GameObject toInstantiate = ennemyTiles[indexEnnemy];
				GameObject instance = Instantiate(toInstantiate,new Vector3(-1.75f+(x*0.3f),2.5f-(y*0.3f),0f),Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
		}
	}
	int GetEnnemyIndex(int row){
		//Mess
		return row < 1? 2:row <3?0:1;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
