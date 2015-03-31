using UnityEngine;
using System.Collections;

public class EnnemyLaser : MonoBehaviour {

	public float velocity= 0.01f;

	void Update () {
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (0, -velocity);
		transform.position = end;
	}

	private void OnTriggerExit2D(Collider2D other){
		Debug.Log("ExitTrigger :" + other.tag);
		if (other.tag == "Finish") {
			Debug.Log ("Destroy");
			Destroy(gameObject);
		}
	}

}
