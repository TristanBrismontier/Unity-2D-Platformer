using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public GameObject player;
	public float offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		float xDiff = transform.x - player.transform.x;
//		if(Mathf.Abs(xDiff)> offset){
//			float toAdd = xDiff >0 ? (xDiff-offset):xDiff+offset ;
//			Vector3 move = Vector3(transform.x + toAdd,0,-10);
//			transform.Translate(move,Space.Self);
//		}
	}
}
