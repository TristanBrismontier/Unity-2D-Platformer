using UnityEngine;
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
	private bool canMove = true;

	void Start() {
		animator = GetComponent<Animator> ();	
		rb = GetComponent<Rigidbody2D>();
		hitCollider = polygoneGame.GetComponent<PolygonCollider2D>();
		hitCollider.enabled = false;
	}
	void FixedUpdate(){
		if(canMove){
			float moveHorizontal = canHit? Input.GetAxis ("Horizontal"):0;
			rb.velocity = new Vector2(moveHorizontal * speed ,rb.velocity.y);
		}

		if (canMove && canJump && Input.GetKey(KeyCode.Space))
		{ 
			float velocityY = rb.velocity.y;
			animator.SetFloat("yvelocity",velocityY);
			canJump = false;
			//Jump Script      
			rb.AddForce(Vector2.up*jumpVelocity,ForceMode2D.Impulse);
			
		}
		bool fg =true;
		if(Input.GetKey(KeyCode.B) && canHit && canJump && canMove){
			animator.SetTrigger("hit"); 
			canHit = false;
			fg=false;
			StartCoroutine (DoAttack());
		}

		if(!animator.GetCurrentAnimatorStateInfo(0).IsName("hit") && !canHit && fg){
			canHit = true;
		}
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
		if(velocityX != 0 && canMove && canHit){
			run = true;
			transform.localScale = new Vector3(velocityX>0?1:-1, 1, 1);
		}
		animator.SetBool("run",run);
		if(canMove){
			animator.SetFloat("yvelocity",velocityY);
		}

		if(transform.position.y < -1){
			Restart ();
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy"){
			if(!canJump || god){
				Destroy(coll.gameObject);
				GameManager.instance.AddEnemy();
			}
			else{
				WasHit(coll.gameObject.transform);
			}			
		}
	}
	void OnCollisionStay2D(Collision2D coll ) 
	{
		if (coll.gameObject.tag == "Ground"){
			canJump = true;
		}
	}

	private void WasHit(Transform ennemy){
		bool stileInLive = GameManager.instance.TakeDamage(1);
		bool direction = Mathf.Abs(GetAngle(transform,ennemy))>90;
		if(stileInLive){
			canMove = false;
			animator.SetTrigger("hurt");
			if(direction){
				rb.AddForce(new Vector2(0.5f,0.5f),ForceMode2D.Impulse);
			}else{
				rb.AddForce(new Vector2(-0.5f,0.5f),ForceMode2D.Impulse);
			}
			transform.localScale = new Vector3(direction?-1:1, 1, 1);
			StartCoroutine (DidHurt());
		}
	}
	private IEnumerator DidHurt () {
		yield return new WaitForSeconds(.2f);
		canMove = true;
	}

	private IEnumerator DoAttack () {
		yield return new WaitForSeconds(.4f);
		god=true;
		hitCollider.enabled = true;
		yield return new WaitForSeconds(.4f);
		god=false;
		hitCollider.enabled = false;
	}

	private float GetAngle(Transform t1, Transform t2)
	{
		float xDiff = t2.position.x - t1.position.x;
		float yDiff = t2.position.y - t1.position.y;
		return Mathf.Atan2(yDiff, xDiff) * (180 / Mathf.PI);
	}



}
