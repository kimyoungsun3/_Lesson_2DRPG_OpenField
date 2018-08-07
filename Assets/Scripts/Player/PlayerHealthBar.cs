using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour {
	public float healthMax = 100;
	float health;
	public Transform transHealthBar;
	Vector3 healthV;

	// Use this for initialization
	//void Start () {
	//	Debug.Log ("1, 2 Plus, Minus");
	//	health = healthMax;
	//}

	public void Init(){
		health = healthMax;
	}

	public void TakeDamage(float _damage){
		health -= _damage;
		if (health < 0) {
			health = 0;
		}else if (health > healthMax) {
			health = healthMax;
		}

		healthV.Set (health / healthMax, 1, 1);
		transHealthBar.localScale = healthV;
	}

	/*
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			health -= 10;
			if (health < 0) {
				health = 0;
			}

			healthV.Set (health / healthMax, 1, 1);
			transHealthBar.localScale = healthV;
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			health += 10;
			if (health > healthMax) {
				health = healthMax;
			}
			healthV.Set (health / healthMax, 1, 1);
			transHealthBar.localScale = healthV;
		}
	}
	*/
}