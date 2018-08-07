using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
	public Transform spawnPoint;
	public Transform nextPortalPoint;

	public Vector3 GetPortalPoint(){
		return nextPortalPoint.GetComponent<Portal> ().spawnPoint.position;
	}
}
