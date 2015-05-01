using UnityEngine;
using System.Collections;

public class CubeSystem : MonoBehaviour {

	public int columns;
	public int rows;
	public GameObject cube;

	void Start () {
		CubeSystemSetup();
	}

	void CubeSystemSetup(){
		for (float x=0; x<columns; x++) {
			for(float y=0; y<rows; y++){
				GameObject toInstantiate = cube;
				GameObject instance = Instantiate(toInstantiate,new Vector3(x*0.1f+transform.position.x,y*0.1f+transform.position.y,0f),Quaternion.identity) as GameObject;
				
			}
		}
	}

}
