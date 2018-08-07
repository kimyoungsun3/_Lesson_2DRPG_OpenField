using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public enum CameraViewMode{Normal, Fixed};
	public CameraViewMode viewMode = CameraViewMode.Normal;
	public float viewSizeDivide = 3f;
	float viewSize = 3f;

	public Transform target;
	public float targetSpeed = .1f;
	Vector3 pos;

	void Start(){
		viewSize = Camera.main.orthographicSize;
	}

	void Update () {

		switch (viewMode) {
		case CameraViewMode.Normal:
			Camera.main.orthographicSize = viewSize;
			break;
		case CameraViewMode.Fixed:
			Camera.main.orthographicSize = Screen.height * .01f * viewSizeDivide;
			break;
		}

		//해당 타켓으로 으로 타켓팅.... 
		if (target != null) {
			pos = Vector3.Lerp (transform.position, target.position, targetSpeed);
			pos.z = transform.position.z;
			transform.position = pos;
		}

	
	}
}
