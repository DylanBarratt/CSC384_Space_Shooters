using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private float health;
	private float speed;

	public void Init(float[] vals) {
		health = vals[0];
		speed = vals[1];
		
		gameObject.SendMessage("Move", speed);
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
