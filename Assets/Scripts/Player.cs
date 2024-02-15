using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private void DamagePlayer(int amount) {
		anime.SetTrigger("Hit");
		health -= amount;

		if (health <= 0) {
			KillPlayer();
		}
		else {
			UIGameObject.SendMessage("UpdateHealth", (float) health / startingHealth * 100);
		}
	}

	private void KillPlayer() { //health 0 checking doesnt happen here so that a player can be killed regardless of health
		gameObject.BroadcastMessage("StopMovement");
		gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
		UIGameObject.SetActive(false);
		
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
