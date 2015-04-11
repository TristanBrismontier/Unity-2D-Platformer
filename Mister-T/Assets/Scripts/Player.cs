using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed;
	public Transform startPosition;
	public float jumpVelocity;
	
	private Rigidbody2D rb;
	private Vector3 start;
	private bool canJump;
	private Animator animator;
	
	
	void Start() {
		animator = GetComponent<Animator> ();	
		rb = GetComponent<Rigidbody2D>();
	}
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		rb.velocity = new Vector2(moveHorizontal * speed ,rb.velocity.y);
		if (canJump && Input.GetKey(KeyCode.Space))
		{ 
			float velocityY = rb.velocity.y;
			animator.SetFloat("yvelocity",velocityY);
			canJump = false;
			//Jump Script      
			rb.AddForce(Vector2.up*jumpVelocity,ForceMode2D.Impulse);
			
		}
	}
	// Update is called once per frame
	void Update () {
		float velocityX = rb.velocity.x;
		int velocityY = (int)(rb.velocity.y * 1000);
		bool run = false;
		if(velocityX != 0){
			run = true;
			transform.localScale = new Vector3(velocityX>0?1:-1, 1, 1);
		}
		animator.SetBool("run",run);
		animator.SetFloat("yvelocity",velocityY);
		if(transform.position.y < -1){
			transform.position = startPosition.position;
		}
	}

	void OnCollisionStay2D(Collision2D coll ) // C#, type first, name in second
	{
		canJump = true;
	}
}
