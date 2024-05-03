using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {
	private GameObject UIGameObject;
	private GameObject Starz;

	private Vector2 targetY = new Vector2(0, 3.5f);
	
	private float speed = 2f; //move to y speed initially
	private bool movingLeft = true;
	private bool yReach, move;
	
	private void Start() {
		UIGameObject = GameObject.Find("HUD_UI");
		Starz = GameObject.Find("Starz/bg");
	}

	private void SetBossSpeed(float val) {
		if (movingLeft) {
			speed = -val; //keep it moving in last direction
			return;
		}
		
		speed = val;
	}
	
	private void Update() {
		if (yReach) {
			if (move) {
				transform.Translate(Vector3.left * Time.deltaTime * speed);			
			}
		} else {
			transform.Translate(Vector3.up * Time.deltaTime * (speed * 2));
		
			if (transform.position.y <= targetY.y) {
				yReach = true;
				Invoke(nameof(YReached), 0.1f);
			}
		}
	}

	private void YReached() {
		gameObject.SendMessage("YReachedInit", speed);
			
		UIGameObject.SendMessage("DisplayBossBar", true);
		
		Starz.SendMessage("SetSpeed", 0.00001f);
		move = true;
	}
	
	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("SBoundary")) {
			speed = -speed;  // neat refactoring came from this cool line
			movingLeft = !movingLeft;
		}
	}
}
