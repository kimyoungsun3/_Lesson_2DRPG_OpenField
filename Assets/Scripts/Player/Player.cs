using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour {
	PlayerController controller;
	PlayerHealthBar healthBar;

	[Header("Camera Focus Target ")]
	public Transform target;

	[Header("Sound ")]
	public AudioClip audioBGM;
	public AudioClip audioAttack;

	[Header("Effect")]
	public ParticleSystem prefabSkillEffect;
	public List<Transform> listPoints = new List<Transform>();
	float attackTime;
	
	void Start () {
		controller 	= GetComponent<PlayerController> ();	
		controller.Init ();

		healthBar 	= GetComponent<PlayerHealthBar> ();	
		healthBar.Init ();

		Debug.Log (" 1. Keyboard Arrow is move");
		Debug.Log (" 2. Num 1 -> Camera Shake");
		Debug.Log (" 3. Num 2, 3 -> Health Bar");
		Debug.Log (" 4. Num 4, 5 -> Focus Camera");
		Debug.Log (" 5. Num Space -> Sound Play, SkillEffect");
		SoundManager.ins.PlayMusic (audioBGM);
	}
	
	// Update is called once per frame
	void Update () {
		if (controller != null) {
			controller.Move ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			CameraShake.ins.CoShake (0.5f, 0.2f);
		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			healthBar.TakeDamage (10f);
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			healthBar.TakeDamage (-10f);
		} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			CameraTargetFocus.ins.InvokeFocuse (target, false);
		} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
			CameraTargetFocus.ins.InvokeFocuse (transform, true);
		} else if (Input.GetKeyDown (KeyCode.Space) && Time.time > attackTime ) {
			attackTime = Time.time + Constant.ATTACK_TIME;
			SoundManager.ins.Play (audioAttack);

			//해방향으로 파티클 생성... 시간되면 자동 소멸...
			int _idx = controller.GetDir ();
			ParticleSystem _particle = Instantiate (prefabSkillEffect, listPoints[_idx].position, Quaternion.identity) as ParticleSystem;
			Destroy (_particle.gameObject, _particle.main.duration);
		}
	}

	//------------------------------------------------
	//Enter, Stay, Exit
	void OnTriggerEnter2D(Collider2D _col){
		//Debug.Log (_col.name + ":");
		if (_col.CompareTag ("Portal")) {
			Portal _portal = _col.GetComponent<Portal> ();
			if (_portal != null) {
				transform.position = _portal.GetPortalPoint ();
			}
		}else if (_col.tag == "Box2") {
			Tilemap _render = _col.GetComponent<Tilemap> ();
			Color _c = _render.color;
			_c.a = .5f;
			_render.color = _c;
		}
	}

	void OnTriggerExit2D(Collider2D _col){
		if (_col.tag == "Box2") {
			Tilemap _render = _col.GetComponent<Tilemap> ();
			Color _c = _render.color;
			_c.a = 1f;
			_render.color = _c;
		}
	}

	//---------------------------------------------------
	//Enter, Stay, Exit
	void OnCollisionEnter2D(Collision2D _col){
		//Debug.Log (_col.collider.name + ":" + _col.collider.tag);
		if (_col.collider.tag == "Box") {
			SpriteRenderer _render = _col.collider.GetComponent<SpriteRenderer> ();
			Color _c = _render.color;
			_c.a = 0.5f;
			_render.color = _c;
		}
	}

	void OnCollisionExit2D(Collision2D _col){
		Debug.Log (_col.collider.name + ":" + _col.collider.tag);
		if (_col.collider.tag == "Box") {
			SpriteRenderer _render = _col.collider.GetComponent<SpriteRenderer> ();
			Color _c = _render.color;
			_c.a = 1f;
			_render.color = _c;
		} 
	}
}
