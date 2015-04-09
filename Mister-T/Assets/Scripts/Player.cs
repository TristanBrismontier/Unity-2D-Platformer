using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed;
	public Transform startPosition;
	public float jumpVelocity;
	
	private Rigidbody2D rb;
	private Vector3 start;
	private bool canJump;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		rb.velocity = new Vector2(moveHorizontal * speed ,rb.velocity.y);
		if (canJump && Input.GetKey(KeyCode.Space))
		{ 
			canJump = false;
			Debug.Log("plop");
			//Jump Script      
			rb.AddForce(Vector2.up*jumpVelocity,ForceMode2D.Impulse);
			
		}
	}

	// Update is called once per frame
	void Update () {
		if(transform.position.y < -1){
			transform.position = startPosition.position;
		}
	}

	void OnCollisionStay2D(Collision2D coll ) // C#, type first, name in second
	{
		Debug.Log(coll.gameObject.tag);
		canJump = true;
	}
}
