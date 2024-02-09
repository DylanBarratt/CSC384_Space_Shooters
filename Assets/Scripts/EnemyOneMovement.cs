using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Screen = UnityEngine.Device.Screen;

public class EnemyOneMovement : MonoBehaviour {
	private float speed;
	private Rigidbody2D rb;

	private float targetY;
	private float moveDelay;

	private bool leftSideReached;
	
	public void Move(float s) {
		speed = s;
		
		rb = GetComponent<Rigidbody2D>();
		
		moveDelay = 0.2f;
		MoveToYPosition();
	}
	
	private void MoveToYPosition() {
		float screenTop = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
		targetY = Random.Range(1f, screenTop - 1f);
		rb.velocity = new Vector2(0, -speed);
		Invoke(nameof(YPosCheck), moveDelay);
	}

	private void YPosCheck() {
		if (rb.position.y <= targetY) {
			rb.velocity = Vector2.zero;
			
			gameObject.SendMessage("ShootStart", 2f);
			
			XPatrolInit();
			
			return;
		}
		
		Invoke(nameof(YPosCheck), moveDelay);
	}

	private void XPatrolInit() {
		//first patrol, move to closest side.	
		if (gameObject.transform.position.x < 0) {
			leftSideReached = false; //move towards left
		}
		else {
			leftSideReached = true; //move towards right
		}
		
		XPatrol();
	}
	private void XPatrol() {
		if (leftSideReached) { //move towards right
			rb.velocity = new Vector2(speed, 0);
		}
		else { //move towards left
			rb.velocity = new Vector2(-speed, 0);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Boundary")) {
			rb.velocity = Vector2.zero;
			leftSideReached = !leftSideReached; //swap to other side
			XPatrol();
		}
	}
}
