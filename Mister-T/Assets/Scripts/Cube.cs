﻿using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = new Vector3(transform.position.x,transform.position.y);	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "CubeSystem"){
			transform.position = startPosition ;
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);

		}
	}
}
