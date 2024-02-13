using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour { 
	private Animator anime;
	private GameObject UIGameObject;
	private PlayerMovement pl;

	private int startingHealth;
	private int health;

	private void Start() {
		startingHealth = 3;
		health = startingHealth;

		UIGameObject = GameObject.Find("GameManager/HUD_UI");
		anime = GetComponent<Animator>();
		pl = GetComponent<PlayerMovement>();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Enemy")) {
			KillPlayer();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		switch (collision.gameObject.tag) {
			case "Asteroid":
				DamagePlayer(1);
				break;
			case "EnemyBullet":
				DamagePlayer(1);
				break;
		}
	}

	private void DamagePlayer(int amount) {
		anime.SetTrigger("Hit");
		health -= amount;
		
		UIGameObject.SendMessage("UpdateHealth", (float) health / startingHealth * 100);

		if (health <= 0) {
			KillPlayer();
		}
	}

	private void KillPlayer() { //health 0 checking doesnt happen here so that a player can be killed regardless of health
		anime.SetTrigger("Ded");
		pl.enabled = false;
		//TODO: end game here as well
		Destroy(gameObject, 1f);
	}
}
