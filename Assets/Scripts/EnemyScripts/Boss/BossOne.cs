using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOne : MonoBehaviour {
	private GameObject asteroidSpawner;
	private GameObject UIGameObject;
	private GameObject gameManager;

	private float health = 5; //number of minis
	// private float health = 1; //TODO: dlete dev halp
	private float startingHealth;

	
	private void Start() {
		startingHealth = health;
		
		gameManager = GameObject.Find("GameManager");
		UIGameObject = GameObject.Find("GameManager/HUD_UI");
		asteroidSpawner = GameObject.Find("GameManager/AsteroidSpawner");

		asteroidSpawner.SendMessage("CanSpawn", false);
		
		UpdateHealthBar();
	}

	private void MiniKilled() {
		health--;
		

		UpdateHealthBar();
		
		if (health == 0) {
			Ded();
		}
	}

	private void UpdateHealthBar() {
		UIGameObject.SendMessage("UpdateBossHealth", (health / startingHealth) * 100);

	}

	private void Ded() {
		UIGameObject.SendMessage("DisplayBossBar", false);
		
		Destroy(GetComponent<BoxCollider2D>());
		Destroy(GetComponent<Rigidbody2D>());
		
		Component[] allComponents = GetComponents(typeof(MonoBehaviour));
		foreach (Component gameObjectComponent in allComponents) Destroy((MonoBehaviour)gameObjectComponent);
		
		gameManager.SendMessage("AddMonies", 20);
		
		gameManager.SendMessage("OpenShop", 1);
		Destroy(gameObject);
	}
}
