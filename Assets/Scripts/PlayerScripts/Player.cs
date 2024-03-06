using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player : MonoBehaviour { 
	private Animator anime;
	private GameObject UIGameObject;
	private PlayerMovement pl;
	private GameObject gameManager;
	
	[SerializeField] private GameObject dedUI;
	
	private int startingHealth;
	private int health;

	private float speed;
	private float rateOfFire;

	private bool ded;

	private void Start() {
		gameManager = GameObject.Find("GameManager");
		UIGameObject = GameObject.Find("GameManager/HUD_UI");
		anime = GetComponent<Animator>();
		pl = GetComponent<PlayerMovement>();
		
		DeletePlayerStats(); //TODO: remove (dev)
		LoadPlayerStats();
	}

	private void LoadPlayerStats() {
		if (!File.Exists(SaveSystem.GetPlayerSavePath())) {
			SaveSystem.SavePlayer(3, 5, 0.2f); //default player stats
		}

		PlayerData player = SaveSystem.LoadPlayer();
		
		startingHealth = player.health;
		health = startingHealth;

		speed = player.speed;
		gameObject.SendMessage("SetSpeed", speed);
		
		rateOfFire = player.rateOfFire;
		gameObject.SendMessage("SetROF", rateOfFire);
	}

	private void SavePlayerStats() {
		SaveSystem.SavePlayer(health, speed, rateOfFire);
	}

	private void DeletePlayerStats() {
		SaveSystem.DeletePlayer();
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
		Destroy(GetComponent<Rigidbody2D>());
		Destroy(GetComponent<CapsuleCollider2D>());
		
		UIGameObject.SendMessage("DisplayBossBar", false);
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
