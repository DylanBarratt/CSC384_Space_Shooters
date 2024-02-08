using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
	private const float BULLET_LIFE = 5f;
	private const float BULLET_FORCE = 2f;

	private Rigidbody2D rb;
    
	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		Shoot();
	}
    
	private void Shoot() {
		rb.AddForce(Vector3.down * BULLET_FORCE, ForceMode2D.Impulse);
		Destroy(gameObject, BULLET_LIFE);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("bullet: " + collision.gameObject.tag);
		switch(collision.gameObject.tag) {
			case "Player":
				Destroy(gameObject);
				break;
			case "Boundary":
				Destroy(gameObject);
				break;
		}
	}
}
