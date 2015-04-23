using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	public GameObject[] hearts; 
	public GameObject gameManager;

	void Awake () {
		if (GameManager.instance == null)
			Instantiate(gameManager);

		GameManager.instance.SetHearts(hearts);
	}
	// Update is called once per frame
	void Update () 
	{
		if (target)
		{
			Vector3 point = GetComponent<UnityEngine.Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<UnityEngine.Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			destination = new Vector3(destination.x, Mathf.Clamp(destination.y,1.9f,100),destination.z);
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
		
	}
}
