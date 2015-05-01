using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player"){
			Debug.Log("CheckPoint");
			GameManager.instance.startPosition = transform;
		}
	}
}
