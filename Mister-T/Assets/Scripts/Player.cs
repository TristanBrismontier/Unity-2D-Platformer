using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed;
	public float jumpVelocity;
	public GameObject polygoneGame;
	public AudioClip jumpSound;
	public AudioClip hitSound;
	public AudioClip smashSound;
	public float jumpRate = 0.5F;

	private Rigidbody2D rb;
	private Vector3 start;
	private bool canJump;
	private Animator animator;
	private PolygonCollider2D hitCollider;
	private BoxCollider2D hitBox;
	private CircleCollider2D hitbox2;
	private bool god = false;
	private bool canHit = true;
	private bool canMove = true;
	private float nextJump = 0.0F;


	void Start() {
		animator = GetComponent<Animator> ();	
		rb = GetComponent<Rigidbody2D>();
		hitCollider = polygoneGame.GetComponent<PolygonCollider2D>();
		hitBox = GetComponent<BoxCollider2D>();
		hitbox2 = GetComponent<CircleCollider2D>();
		hitCollider.enabled = false;
	}


	void FixedUpdate(){
		if(canMove){
			Move ();
		}

		if (canMove && canJump && Input.GetKey(KeyCode.Space) && Time.time > nextJump) {
			nextJump = Time.time + jumpRate; 
			Jump ();
		}
		bool fg =true;
		if(Input.GetKey(KeyCode.B) && canHit && canJump && canMove){
			Hit (); 
			fg = false;
		}
		if(!animator.GetCurrentAnimatorStateInfo(0).IsName("hit") && !canHit && fg){
			canHit = true;
		}
	}

	private void Move ()
	{
		float moveHorizontal = canHit ? Input.GetAxis ("Horizontal") : 0;
		rb.velocity = new Vector2 (moveHorizontal * speed, rb.velocity.y);
	}
	
	private void Jump ()
	{
		if (Input.GetAxis ("Vertical") < 0 && rb.position.y > 1) {
			hitBox.isTrigger = true;
			hitbox2.isTrigger = true;
		}
		else {
			float velocityY = rb.velocity.y;
			animator.SetFloat ("yvelocity", velocityY);
			//Jump Script      
			rb.AddForce (Vector2.up * jumpVelocity, ForceMode2D.Impulse);
		}
		canJump = false;
		SoundManager.instance.PlaySingle(jumpSound);
	}

	private void Hit ()
	{
		animator.SetTrigger ("hit");
		canHit = false;
		StartCoroutine (DoAttack ());
	}



	// Update is called once per frame
	void Update () {
		float velocityX = rb.velocity.x;
		int velocityY = (int)(rb.velocity.y * 1000);
		bool run = false;
		if(Mathf.Abs (velocityX) > 0.0001 && canMove && canHit){
			run = true;
			transform.localScale = new Vector3(velocityX>0?1:-1, 1, 1);
		}
		animator.SetBool("run",run);
		if(canMove){
			animator.SetFloat("yvelocity",velocityY);
		}

		if(transform.position.y < -1){
			GameManager.instance.TakeDamage(3);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy"){
			if(!canJump || god){
				Debug.Log ( "Jump "+canJump+" God : "+ god);
				Destroy(coll.gameObject);
				GameManager.instance.AddEnemy();
			}
			else{
				WasHit(coll.gameObject.transform);
			}			
		}
		if (coll.gameObject.tag == "DeadlyThing"){
			WasHit(coll.gameObject.transform);
		}
	}
	void OnCollisionStay2D(Collision2D other ) 
	{
		if (other.gameObject.tag == "Ground" &&  Time.time > nextJump){
			canJump = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy" && god){
			Destroy(other.gameObject);
			GameManager.instance.AddEnemy();
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Ground"){
			hitBox.isTrigger = false;
			hitbox2.isTrigger = false;
		}
		Debug.Log ("ExitTrigger");
		if(other.gameObject.tag == "platform"){
			transform.parent = null;
			
		}
	}

	void OnTriggerStay2D(Collider2D other){
		Debug.Log ("EnterTrigger");
		if(other.gameObject.tag == "platform"){
			transform.parent= other.transform;
			
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
			SoundManager.instance.PlaySingle(hitSound);
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
		SoundManager.instance.PlaySingle(smashSound);
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
