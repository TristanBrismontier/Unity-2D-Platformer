using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed;
	public Transform startPosition;
	
	private Rigidbody2D rb;
	private Vector3 start;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 0.0f); 
		transform.position += movement * speed * Time.deltaTime;
	}

	// Update is called once per frame
	void Update () {
		if(transform.position.y < -1){
			transform.position = startPosition.position;
		}
	
	}
}
