using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private bool initiated;
	
	private float health, speed, value;

	public void EnemyInit(float[] vals) {
		health = vals[0];
		speed = vals[1];
		value = vals[2];

		//if not boss enemy
		if (value != 0 && speed != 0) {
			gameObject.SendMessage("Move", speed);
			gameObject.SendMessage("SetValue", value);
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
