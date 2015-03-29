using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public float velocity= 0.01f;

	private Player player;

	void Start(){
		player =(Player)FindObjectOfType(typeof(Player));
		if(!player){
			Debug.LogError("Pas Player");
		}

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
		player.CanFireAgain ();;
	}
}
