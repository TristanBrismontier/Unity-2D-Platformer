using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public GameObject player;
	public float offset;
	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	
	// Update is called once per frame
	void Update () 
	{
		if (target)
		{
			Vector3 point = GetComponent<UnityEngine.Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<UnityEngine.Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
		
	}
//	
//	// Update is called once per frame
//	void Update () {
//		float xDiff = transform.position.x - player.transform.position.x;
//		if(Mathf.Abs(xDiff)> offset){
//			float toAdd = xDiff < 0 ? speed:-speed ;
//			transform.Translate( new Vector3(toAdd,0,0),Space.Self);
//
//		}
//	}
}
