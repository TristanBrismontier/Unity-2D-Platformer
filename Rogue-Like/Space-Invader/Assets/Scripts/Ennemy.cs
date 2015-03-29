using UnityEngine;
using System.Collections;

public class Ennemy : MonoBehaviour {


	public SpriteRenderer sprite1;
	public SpriteRenderer sprite2;
	public Transform shotSpawn;
	public int score; 

	public GameObject laser;

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
			GameManager.instance.addEnnemyDestroyScore(score,this);
			Destroy(other.gameObject);
			Destroy (gameObject);
		} else if (other.tag == "Player"){
			GameManager.instance.GameOver ();
		}
	}

	private void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Border") {
			GameManager.instance.EnnemyContactBorder();
		}
	}

	void Update() {
		if (Random.value >0.9995f && !GameManager.instance.gameOver) {// && canFire ){
			Instantiate(laser,shotSpawn.position,shotSpawn.rotation);
		}
	}
}
