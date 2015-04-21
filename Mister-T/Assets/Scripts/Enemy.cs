using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour {
	public float speed;
	public float maxDetection;

	private Transform target;
	private Rigidbody2D rb;
	private bool face = false;
	private Animator animator;

	void Start () {
		animator = GetComponent<Animator> ();	
		rb = GetComponent<Rigidbody2D>();
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
	}
	
	// Update is called once per frame
	void Update () {
		float angle = Mathf.Abs(GetAngle(transform,target));
		if(Vector2.Distance(target.position,transform.position)<maxDetection && (angle>100 || angle <80)){
			face = angle < 90;
			animator.SetBool("run",true);
			transform.localScale = new Vector3(face?1:-1, 1, 1);
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

	private float GetAngle(Transform t1, Transform t2)
	{
		float xDiff = t2.position.x - t1.position.x;
		float yDiff = t2.position.y - t1.position.y;
		return Mathf.Atan2(yDiff, xDiff) * (180 / Mathf.PI);
	}
}
