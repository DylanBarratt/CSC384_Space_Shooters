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

	private int initialHealth = 3;
	private int fullHealthAmnt;
	private int health;

	private float speed;
	private float rateOfFire;
	private float intialSpeed = 4f;
	private float initialROF = 0.2f;

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
			SaveSystem.SavePlayer(initialHealth, intialSpeed, initialROF); //default player stats
		}

		PlayerData player = SaveSystem.LoadPlayer();
		
		health = player.health;
		fullHealthAmnt = health;

		speed = player.speed;
		gameObject.SendMessage("SetSpeed", speed);
		
		rateOfFire = player.rateOfFire;
		gameObject.SendMessage("SetROF", rateOfFire);
	}

	private void AddHealth(int amnt) {
		health = fullHealthAmnt; //heal player
		health += amnt;

		if (health >= 13) {
			Debug.Log("TOOO HEALTHY DUD");
			return;
		}
		
		fullHealthAmnt = health;
		UpdateHealth();
		
		SavePlayerStats();
	}

	private void UpdateHealth() {
		UIGameObject.SendMessage("UpdateHealth", (float) health / initialHealth * 100);
	}
	
	private void AddSpeed(int amnt) {
		speed += amnt;

		if (speed > 24) {
			Debug.Log("TOOO MUCH SPEEEED!!");
			return;
		}
		
		gameObject.SendMessage("SetSpeed", speed);
		SavePlayerStats();
	}
	
	private void AddROF(int amnt) {
		rateOfFire += amnt;

		if (rateOfFire <= 0.1f) {
			Debug.Log("TOO MUCH POWEWEERR");
			return;
		} 
		
		gameObject.SendMessage("SetROF", rateOfFire);
		SavePlayerStats();
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
			UpdateHealth();
		}
	}

	private void KillPlayer() { //health 0 checking doesnt happen here so that a player can be killed regardless of health
		Destroy(GetComponent<Rigidbody2D>());
		Destroy(GetComponent<CapsuleCollider2D>());
		
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
