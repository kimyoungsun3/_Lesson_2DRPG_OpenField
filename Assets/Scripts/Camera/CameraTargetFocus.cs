using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTargetFocus : MonoBehaviour {
	public static CameraTargetFocus ins  { get; private set; }
	CinemachineBrain scpCinemachineBrain;
	float originalZ;
	public float speedLookAt = 2f;
	//bool bMoving = false;
	Vector3 originalPos;
	bool bBeforePlayer = true;

	void Awake(){
		ins = this;

		scpCinemachineBrain = GetComponent<CinemachineBrain> ();
		originalZ 			= transform.position.z;
		enabled 			= false;
	}

	public void InvokeFocuse(Transform _target, bool _bPlayer){
		//Debug.Log(_target.position + ":" + transform.position);
		//if (bMoving)
		//	return;

		if (scpCinemachineBrain != null) {
			scpCinemachineBrain.enabled = false;
		}

		if(!_bPlayer && bBeforePlayer){
			originalPos = transform.position;
		}
		bBeforePlayer = _bPlayer;

		StopAllCoroutines ();
		StartCoroutine (CoFocus (_target, _bPlayer));
	}

	IEnumerator CoFocus(Transform _target, bool _bPlayer){		
		//bMoving = true;
		Vector3 _pos 	= transform.position;
		Vector3 _pos2 	= _target.position;
		if (_bPlayer) {
			_pos2 = originalPos;
		}
		float _interpolate = 0;
		_pos.z 	= originalZ;
		_pos2.z = originalZ;

		while (_interpolate < 1f) {
			_interpolate 	+= speedLookAt * Time.deltaTime;
			_pos 			= Vector3.Lerp (_pos, _pos2, _interpolate);
			transform.position = _pos;
			yield return null;
		}
		transform.localPosition = _pos2;


		if (_bPlayer && scpCinemachineBrain != null ) {
			scpCinemachineBrain.enabled = true;
		}
		//bMoving = false;
	}
}
