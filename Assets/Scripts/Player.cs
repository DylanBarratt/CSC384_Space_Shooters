using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour { 
	private Animator anime;
	private GameObject UIGameObject;
	private PlayerMovement pl;
	private GameObject gameManager;
	
	[SerializeField] private GameObject dedUI;
	
	private int startingHealth;
	private int health;

	private bool ded;

	private void Start() {
		startingHealth = 3;
		health = startingHealth;
		ded = false;

		gameManager = GameObject.Find("GameManager");
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
		gameObject.BroadcastMessage("StopMovement");
		
		anime.SetTrigger("Ded");
		pl.enabled = false;

		if (dedUI != null) {
			Invoke(nameof(EnableContinue), 1.5f); // to rub ded into the player :D
			dedUI.SetActive(true);
		}
	}

	private void Update() {
		if (ded) {
			if (Input.anyKeyDown) {
				EndGame();
			}
		}
	}

	private void EndGame() {
		gameManager.SendMessage("EndGame");
	}

	private void EnableContinue() {
		ded = true;
	}
}
