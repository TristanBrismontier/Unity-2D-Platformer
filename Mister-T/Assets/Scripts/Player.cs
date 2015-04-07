using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed;

	private Rigidbody2D rb;
	private Vector3 start;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		start = new Vector3(0.5f,0,0);
	}
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 0.0f); 
		transform.position += movement * speed * Time.deltaTime;
		//rb.velocity = movement * speed;
	}

	// Update is called once per frame
	void Update () {
		if(transform.position.y < -1){
			transform.position = start;
		}
	
	}
}
