using UnityEngine;
using System.Collections;

public class Ennemy : MonoBehaviour {


	public SpriteRenderer sprite1;
	public SpriteRenderer sprite2;
	public int score; 

	private bool anim= true;

	void AnimationAnim ()
	{
		sprite1.enabled = anim;
		sprite2.enabled = !anim;
	}

	// Use this for initialization
	void Start () {
		GameManager.instance.addEnemyToList (this);
		AnimationAnim ();
	}

	public void Move(float xDir, float yDir){
		anim = !anim;
		AnimationAnim ();
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);
		transform.position = end;
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Laser") {
			GameManager.instance.addEnnemyDestroyScore(score);
			Destroy(other.gameObject);
			gameObject.SetActive(false);
		}
	}

	private void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Border") {
			GameManager.instance.EnnemyContactBorder();
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
