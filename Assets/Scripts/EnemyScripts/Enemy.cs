using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private float health, speed, value;

	public void EnemyInit(float[] vals) {
		health = vals[0];
		speed = vals[1];
		value = vals[2];
		
		gameObject.SendMessage("Move", speed);
		gameObject.SendMessage("SetValue", value);
	}
	
	private void OnTriggerEnter2D(Collider2D collision) {
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
		health -= amount;
		
		if (health <= 0) {
			gameObject.SendMessage("KillEnemy");
		}
	}
}
