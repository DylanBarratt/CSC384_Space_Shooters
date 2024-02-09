using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private int health;

	private void Start() {
		health = 2;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		switch (collision.gameObject.tag) {
			case "Asteroid":
				DamagePlayer(1);
				break;
			case "EnemyBullet":
				DamagePlayer(1);
				break;
			case "Enemy":
				KillPlayer();
				break;
		}
	}

	private void DamagePlayer(int amount) {
		health -= amount;

		if (health <= 0) {
			KillPlayer();
		}
	}

	private void KillPlayer() { //health 0 checking doesnt happen here so that a player can be killed regardless of health
		Destroy(gameObject);
	}
}
