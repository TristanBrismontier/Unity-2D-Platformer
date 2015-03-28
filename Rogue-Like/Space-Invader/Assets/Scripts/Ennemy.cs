using UnityEngine;
using System.Collections;

public class Ennemy : MonoBehaviour {


	public SpriteRenderer sprite1;
	public SpriteRenderer sprite2;
	private bool anim;

	void AnimationAnim ()
	{
		sprite1.enabled = anim;
		sprite2.enabled = !anim;
	}

	// Use this for initialization
	void Start () {
		anim = true;
		GameManager.instance.addEnemyToList (this);
		if(sprite1 == null){
			Debug.Log("Sprites1 == Null");
		}
		if(sprite2 == null){
			Debug.Log("Sprites1 == Null");
		}

		AnimationAnim ();
	}

	public void Move(float xDir, float yDir){
		anim = !anim;
		AnimationAnim ();
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);
		transform.position = end;
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
