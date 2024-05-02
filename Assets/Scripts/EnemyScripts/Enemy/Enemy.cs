using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private bool initiated;

	[SerializeField] private EnemyType enemyStats;
	private float health;
	private void Start() {
		health = enemyStats.health;
	}

	public void EnemyInit() {
		//if not boss enemy
		if (enemyStats.speed != 0 && enemyStats.value != 0) {
			gameObject.SendMessage("Move", enemyStats.speed);			
			gameObject.SendMessage("SetValue", enemyStats.value);
		}

		initiated = true;
	}
	
	private void OnTriggerEnter2D(Collider2D collision) {
		if (!initiated) return;
		
		switch (collision.gameObject.tag) {
			case "PlayerBullet":
				Damage(1);
				break;
			case "Player":
				gameObject.SendMessage("KillEnemy");
				break;
		}
	}

	private void Damage(int amount) {
		if (!initiated) return;

		health -= amount;
		
		if (health <= 0) {
			gameObject.SendMessage("KillEnemy");
		}
	}
}
