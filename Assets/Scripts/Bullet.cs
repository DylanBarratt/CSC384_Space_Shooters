using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private float force;

	private Rigidbody2D rb;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		
		if (gameObject.CompareTag("EnemyBullet")) {
			force = -10f;
		}
		else {
			force = 20f;
		}
		
		rb.AddForce(Vector3.up * force, ForceMode2D.Impulse);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		switch (collision.gameObject.tag) {
			case "Asteroid":
				Destroy(gameObject);
				break;
			case "Boundary":
				Destroy(gameObject);
				break;
		}
	}
}
