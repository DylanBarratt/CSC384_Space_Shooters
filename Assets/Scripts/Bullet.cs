using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private float force;
	private float destroyDelay = 2f;
	
	private Rigidbody2D rb;

	private void SetScale(float scale) {
		gameObject.transform.localScale = new Vector2(scale, scale);
	}

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
				DestroyBullet();
				break;
			case "Boundary":
				Invoke(nameof(DestroyBullet), destroyDelay);
				break;
			case "UBoundary":
				Invoke(nameof(DestroyBullet), destroyDelay);
				break;
		}
	}

	private void DestroyBullet() {
		Destroy(gameObject);
	}
}
