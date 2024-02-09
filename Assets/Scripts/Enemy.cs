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
	
	private void OnTriggerEnter2D(Collider2D collision) {
		switch (collision.gameObject.tag) {
			case "Asteroid":
				Damage(1);
				break;
			case "PlayerBullet":
				Damage(1);
				break;
			case "Player":
				KillEnemy();
				break;
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
