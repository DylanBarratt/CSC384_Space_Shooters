using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private GameObject enemySpawner;
	
	private float health;
	private float speed;

	public void Init(float[] vals) {
		enemySpawner = GameObject.Find("GameManager/EnemySpawner");
		if (enemySpawner == null) {
			Debug.LogError("enemyspawner not found?");
		}
			
		health = vals[0];
		speed = vals[1];
		
		gameObject.SendMessage("Move", speed);
	}
	
	private void OnTriggerEnter2D(Collider2D collision) {
		switch (collision.gameObject.tag) {
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

		if (enemySpawner != null) {
			enemySpawner.SendMessage("EnemyDestroyed");
		}
	}
}
