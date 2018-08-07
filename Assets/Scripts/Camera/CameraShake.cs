using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour {
	public static CameraShake ins  { get; private set; }
	CinemachineBrain scpCinemachineBrain;

	void Awake(){
		ins = this;
	}


	public void CoShake(float _duration, float _size){
		//Cinemachine Controller just moment off
		if (scpCinemachineBrain == null) {
			scpCinemachineBrain = GetComponent<CinemachineBrain> ();
		}

		if (scpCinemachineBrain != null) {
			scpCinemachineBrain.enabled = false;
		}

		StopAllCoroutines ();
		StartCoroutine (Shake (_duration, _size));
	}

	IEnumerator Shake(float _duration, float _size){
		Vector3 _pos = transform.localPosition;
		float _time = Time.time + _duration;
		Vector3 _rand;

		while (Time.time < _time) {
			_rand = Random.onUnitSphere * _size;
			_rand.z = 0;
			transform.localPosition = _pos + _rand;

			yield return null;
		}
		transform.localPosition = _pos;

		if (scpCinemachineBrain != null) {
			scpCinemachineBrain.enabled = true;
		}
	}
}
