using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Boundary 
{
	public float xMin,xMax;
}

public class Player : MonoBehaviour {

	public float speed;
	public Boundary boundary;

	public Text posit;
	public Text triggerBorder;

	public GameObject laser;
	public Transform shotSpawn;

	private Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical); 
		rb.velocity = movement * speed;
		rb.position = new Vector3 
			(
				Mathf.Clamp(rb.position.x,boundary.xMin,boundary.xMax),
				0,
				0
				);
		posit.text = "x: " + rb.position.x + " y: "+ rb.position.y;
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Border") {
			triggerBorder.text = "Touche";
		}
	}

	private void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Border") {
			triggerBorder.text = "Sort";
		}
	}
	
	void Update() {
		if (Input.GetButton("Fire1")) {//&& GameManager.instance.canFire) {
			GameManager.instance.canFire = false;
			Instantiate(laser,shotSpawn.position,shotSpawn.rotation);
		}
	}
}
