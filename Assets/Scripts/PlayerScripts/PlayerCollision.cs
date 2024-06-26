using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Enemy")) {
			gameObject.BroadcastMessage("KillPlayer");
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		switch (collision.gameObject.tag) {
			case "Asteroid":
				Dmg(1);
				break;
			case "EnemyBullet":
				Dmg(1);
				break;
		}
	}

	private void Dmg(int amount) {
		gameObject.BroadcastMessage("DamagePlayer", amount);
	}
}
