using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
	private GameObject asteroidSpawner;
	private GameObject UIGameObject;
	private GameObject gameManager;

	private void Start() {
		gameManager = GameObject.Find("GameManager");
		UIGameObject = GameObject.Find("GameManager/HUD_UI");
		asteroidSpawner = GameObject.Find("GameManager/AsteroidSpawner");

		asteroidSpawner.SendMessage("CanSpawn", false);
	}
	
	private void Ded(int value) {
		UIGameObject.SendMessage("DisplayBossBar", false);
		
		Destroy(GetComponent<BoxCollider2D>());
		Destroy(GetComponent<Rigidbody2D>());
		
		Component[] allComponents = GetComponents(typeof(MonoBehaviour));
		foreach (Component gameObjectComponent in allComponents) Destroy((MonoBehaviour)gameObjectComponent);
		
		gameManager.SendMessage("AddMonies", value);
		
		gameManager.SendMessage("OpenShop", 1);
		Destroy(gameObject);
	}
	
	//startingHealth, health
	private void UpdateHealthBar(float[] healthVals) {
		UIGameObject.SendMessage("UpdateBossHealth", (healthVals[1] / healthVals[0]) * 100);
	}
}
