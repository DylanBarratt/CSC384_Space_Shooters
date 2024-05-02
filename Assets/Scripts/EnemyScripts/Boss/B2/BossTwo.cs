using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwo : MonoBehaviour {
	private int value = 30;
	private float health = 50;
	// private float health = 1; //TODO: dlete dev halp
	private float startingHealth;

	private bool initiated;

	
	private void Start() {
		initiated = false;
		startingHealth = health;
		
		SendMessage("UpdateHealthBar", new float[] {startingHealth, health});
	}

	private void YReachedInit(float speed) {
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

	private void Damage(int amnt) {
		health -= amnt;
		SendMessage("UpdateHealthBar", new float[] {startingHealth, health});

		if (health <= 0) {
			SendMessage("Ded", value);
		}
		
	}
}
