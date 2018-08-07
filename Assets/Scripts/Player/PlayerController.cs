using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	Animator animator;
	Rigidbody2D rb2D;
	Vector2 move, moveBackup;
	public float moveSpeed = 2f;

	public void Init () {
		animator 	= GetComponent<Animator> ();
		rb2D 		= GetComponent<Rigidbody2D> ();
	}

	public void Move(){
		//Debug.Log (11);
		move.Set (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		if (move != Vector2.zero) {
			animator.SetBool ("PlayerWalk", true);
			animator.SetFloat ("PlayerInput_x", move.x);
			animator.SetFloat ("PlayerInput_y", move.y);
			rb2D.MovePosition (rb2D.position + move.normalized * moveSpeed * Time.deltaTime);
			//rb2D.position = rb2D.position + move.normalized * moveSpeed * Time.deltaTime;
			//transform.Translate( move.normalized * moveSpeed * Time.deltaTime);
			//transform.position +=  move.normalized * moveSpeed * Time.deltaTime;
			moveBackup = move;
		} else {
			animator.SetBool ("PlayerWalk", false);
		}

		//move state check
		if (rb2D.velocity != Vector2.zero) {
			rb2D.velocity = Vector2.zero;
		}
	}

	//-------------------------------
	//LU(0)    up (1)   RU(2)
	//L (3)    M(4)     R (5)
	//LD(6)    down(7)  RD(8)
	//-------------------------------
	public int GetDir(){
		int _idx = 4;
		if (moveBackup.x == -1 && moveBackup.y == 1)
			_idx = 0;
		else if (moveBackup.x == 0 && moveBackup.y == 1)
			_idx = 1;
		else if (moveBackup.x == 1 && moveBackup.y == 1)
			_idx = 2;
		
		else if (moveBackup.x == -1 && moveBackup.y == 0)
			_idx = 3;
		else if (moveBackup.x == 0 && moveBackup.y == 0)
			_idx = 4;
		else if (moveBackup.x == 1 && moveBackup.y == 0)
			_idx = 5;
		
		else if (moveBackup.x == -1 && moveBackup.y == -1)
			_idx = 6;
		else if (moveBackup.x == 0 && moveBackup.y == -1)
			_idx = 7;
		else //if (moveBackup.x == 1 && moveBackup.y == -1)
			_idx = 8;

		return _idx;
	}
}
