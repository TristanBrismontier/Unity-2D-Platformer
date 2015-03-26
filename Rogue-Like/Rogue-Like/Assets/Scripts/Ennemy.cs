using UnityEngine;
using System.Collections;

public class Ennemy : MovingObjects  {

	public int playerDamage = 1;


	private Animator animator;
	private Transform target;
	private bool skipMove;
	
	protected override void  Start () {
		GameManager.instance.addEnemyToList (this);
		animator = GetComponent<Animator> ();	
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		base.Start ();
	}
	// Update is called once per frame
	void Update () {
	
	}

	protected override void AttemptMove <T> (int xDir, int yDir){
		if (skipMove) {
			skipMove = false;
			return;
		}

		base.AttemptMove<T> (xDir, yDir);

		skipMove = true;
	}

	public void MoveEnnemy(){
		int xDir = 0;
		int yDir = 0;

		if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
			yDir = target.position.y > transform.position.y ? 1 : -1;
		else
			xDir = target.position.x > transform.position.x ? 1 : -1;

		AttemptMove<Playor> (xDir, yDir);
	}

	protected override void OnCantMove <T> (T component){
		Playor hitPlayer = component as Playor;
		animator.SetTrigger ("ennemyAttack");
		hitPlayer.LoseFood (playerDamage);
	}
}
