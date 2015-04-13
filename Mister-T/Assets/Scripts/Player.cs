﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed;
	public Transform startPosition;
	public float jumpVelocity;
	public GameObject polygoneGame;

	private Rigidbody2D rb;
	private Vector3 start;
	private bool canJump;
	private Animator animator;
	private PolygonCollider2D hitCollider;
	private bool god = false;
	private bool canHit = true;
	static int atakState = Animator.StringToHash("hit");  

	void Start() {
		animator = GetComponent<Animator> ();	
		rb = GetComponent<Rigidbody2D>();
		hitCollider = polygoneGame.GetComponent<PolygonCollider2D>();
		hitCollider.enabled = false;
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
		bool fg =true;
		if(Input.GetKey(KeyCode.B) && canHit && !hitCollider.enabled){
			animator.SetTrigger("hit"); 
			canHit = false;
			fg=false;
			Invoke("DoAttack", .4f);
		}
		if(!animator.GetCurrentAnimatorStateInfo(0).IsName("hit") && !canHit && fg){
			Debug.Log ("canHit");
			canHit = true;
		}
	}
	private void DoAttack(){
		Debug.Log("DoAttaCK");
		Invoke("EnDAttack", .5f);
		god=true;
		hitCollider.enabled = true;
	}

	private void EnDAttack(){
		Debug.Log("EnDAttack");
		god=false;
		hitCollider.enabled = false;
	}

	void Restart()
	{
		transform.position = startPosition.position;
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
			Restart ();
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy"){
			if(!canJump || god){
				Destroy(coll.gameObject);
			}
			else{
				Restart ();
			}			
		}
	}


	void OnCollisionStay2D(Collision2D coll ) // C#, type first, name in second
	{
		canJump = true;
	}
}
