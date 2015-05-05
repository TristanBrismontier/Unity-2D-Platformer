﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour {

	public enum FollowType{
		MoveTowards,
		Lerp
	}
	public FollowType Type = FollowType.MoveTowards;
	public PathView Path;
	public float Speed =1;
	public float MaxDistToGoal = .2f;

	private IEnumerator<Transform> _currentPoint;

	public void Start(){
		if(Path == null){
			Debug.LogError("PathError Null", gameObject);
			return;
		}

		_currentPoint =  Path.GetPaths();
		_currentPoint.MoveNext();

		if(_currentPoint.Current == null){
			Debug.LogError("ErrorStart");
			return;
		}
		transform.position = _currentPoint.Current.position;
	}


	public void Update(){
		if(_currentPoint == null || _currentPoint.Current== null){
			Debug.LogError("Error");
			return;
		}
			
		Debug.Log (transform.position +" -> "+_currentPoint.Current.position);
		if(Type == FollowType.MoveTowards)
			transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
		else if(Type == FollowType.Lerp)
			transform.position = Vector3.Lerp (transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
		float distance = (transform.position - _currentPoint.Current.position).sqrMagnitude;
	
		if(distance < MaxDistToGoal * MaxDistToGoal){
			Debug.Log ("Change");
			_currentPoint.MoveNext();
		}

	}


}
