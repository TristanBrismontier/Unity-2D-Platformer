using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player"){
			GameManager.instance.startPosition = transform;
		}
	}
}
