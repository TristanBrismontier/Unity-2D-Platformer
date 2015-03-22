using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	public float lifeTime;
	void Start () {
		Destroy (gameObject, lifeTime);
	}
}
