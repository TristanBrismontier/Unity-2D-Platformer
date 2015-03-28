using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public float velocity= 0.01f;

	private Player player;
	// Use this for initialization
	void Start () {
	
	}
	public void SetPlayer(Player _player){
		player = _player;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (0, velocity);
		transform.position = end;
	}

	private void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Border") {
			Destroy(gameObject);
		}
	}

	void OnDestroy(){
		GameManager.instance.canFire = true;
	}

}
