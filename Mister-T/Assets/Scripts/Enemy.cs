﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3(-1, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < -1){
			Die ();
		}
	}

	private void Die(){
		Destroy(this.gameObject);
		GameManager.instance.AddEnemy();
	}

}
