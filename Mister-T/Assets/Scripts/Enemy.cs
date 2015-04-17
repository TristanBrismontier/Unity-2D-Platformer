using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float speed;
	public float maxDetection;

	private Transform target;
	private Rigidbody2D rb;
	private bool face;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();	
		rb = GetComponent<Rigidbody2D>();
		face = Random.Range(-1,1)>0;
		transform.localScale = new Vector3(face?1:-1, 1, 1);
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(target.position, transform.position, Color.red); 

		if(Vector2.Distance(target.position,transform.position)<maxDetection){
			animator.SetBool("run",true);
			rb.velocity = new Vector2((face?1:-1)*speed ,rb.velocity.y);
		}else{
			animator.SetBool("run",false);
			rb.velocity = new Vector2(0 ,rb.velocity.y);
		}

		if(transform.position.y < -1){
			Die ();
		}
	}

	private void Die(){
		Destroy(this.gameObject);
		GameManager.instance.AddEnemy();
	}

}
