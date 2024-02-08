using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : MonoBehaviour {
	private float speed;
	private Rigidbody2D rb;

	private float targetY;
	private float moveDelay;
	
	public void Move(float s) {
		speed = s;
		
		rb = GetComponent<Rigidbody2D>();
		moveDelay = 0.2f;
		MoveToYPosition();
	}
	
	private void MoveToYPosition() {
		targetY = 3;
		rb.velocity = new Vector2(0, -speed);
		Invoke(nameof(YPosCheck), moveDelay);
	}

	private void YPosCheck() {
		if (rb.position.y <= targetY) {
			rb.velocity = Vector2.zero;
			Debug.Log("Arrived :DD");
			return;
		}
		
		Invoke(nameof(YPosCheck), moveDelay);
	}
}
