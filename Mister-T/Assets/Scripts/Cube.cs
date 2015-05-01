using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	private Transform startPosition;
	// Use this for initialization
	void Start () {
		startPosition = transform;	
	}
	
	// Update is called once per frame
	void Update () {
		float x = transform.position.x;
		float y = transform.position.y;
		Debug.Log( x + " " + y);
		if( x < 0 || x > 100 || y > 10 || y < -2 ){
			transform.position = startPosition.position;
		}
	}
}
