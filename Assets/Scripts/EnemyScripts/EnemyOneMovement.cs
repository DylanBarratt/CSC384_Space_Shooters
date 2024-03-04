using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Screen = UnityEngine.Device.Screen;

public class EnemyOneMovement : MonoBehaviour {
	private Rigidbody2D rb;

	private float speed;
	
	private void YReached(float s) {
		speed = s;
		rb = GetComponent<Rigidbody2D>();
		
		gameObject.SendMessage("Shoot");
		Invoke(nameof(XPatrol), 0.01f);
	}
	
	private void XPatrol() {
		rb.velocity = new Vector2(speed, 0);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("SBoundary")) {
			rb.velocity = Vector2.zero;
			speed = -speed; //some very neat refactoring came from this cool line !
			Invoke(nameof(XPatrol), 0.01f);
		}
	}
}
