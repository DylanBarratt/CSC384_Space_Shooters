using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private int health;
	private float speed;

	public void Init(int h, float s) {
		health = h;
		speed = s;
		
		gameObject.SendMessage("Move", speed);
	}

	private void Start() {
		Init(3, 2); //should be called when enemy is instantiated not here
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Bullet")) {
			Damage(1);
		}
	}

	private void Damage(int amount) {
		health -= amount;

		if (health <= 0) {
			KillEnemy();
		}
	}

	private void KillEnemy() {
		Destroy(gameObject);
	}
}
