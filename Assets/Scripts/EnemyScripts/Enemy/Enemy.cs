using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private bool initiated;

	private EnemyData stats;

	public void EnemyInit(EnemyData vals) {
		stats = new EnemyData(vals.health, vals.speed, vals.value);

		//if not boss enemy
		if (stats.speed != 0 && stats.value != 0) {
			gameObject.SendMessage("Move", stats.speed);			
			gameObject.SendMessage("SetValue", stats.value);
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

		stats.health -= amount;
		
		if (stats.health <= 0) {
			gameObject.SendMessage("KillEnemy");
		}
	}
}
