using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	private Rigidbody2D rb;

	private float speed;
	private float targetY;
	private float moveDelay;
	
	private void Move(float s) {
		speed = s;
		
		rb = GetComponent<Rigidbody2D>();
		
		moveDelay = 0.2f;
		MoveToYPosition();
	}
	
	private void MoveToYPosition() {
		float screenTop = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
		targetY = Random.Range(1f, screenTop - 1f);
		rb.velocity = new Vector2(0, -(speed * 1.5f));
		Invoke(nameof(YPosCheck), moveDelay);
	}

	private void YPosCheck() {
		if (rb.position.y <= targetY) {
			rb.velocity = Vector2.zero;
			gameObject.SendMessage("YReached", speed);
			
			return;
		}
		
		Invoke(nameof(YPosCheck), moveDelay);
	}

	private void StopY() {
		CancelInvoke();
	}
}
