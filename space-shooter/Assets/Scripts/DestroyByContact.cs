using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gC;

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gC = gameControllerObject.GetComponent<GameController> ();
		} else {
			Debug.Log ("GameController  NotFound");
		}
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary") {
			return;
		}
		Instantiate(explosion,transform.position,transform.rotation);
		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gC.playerDead ();
		} else {
			gC.addScore (scoreValue);
		}
		Destroy(other.gameObject);
		Destroy (gameObject);
	}
}
