using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOne : MonoBehaviour {
	private GameObject asteroidSpawner;
	private GameObject UIGameObject;
	private GameObject gameManager;

	private float health = 5;

	private void Start() {
		gameManager = GameObject.Find("GameManager");
		UIGameObject = GameObject.Find("GameManager/HUD_UI");
		asteroidSpawner = GameObject.Find("GameManager/AsteroidSpawner");

		asteroidSpawner.SendMessage("CanSpawn", false);
	}

	private void MiniKilled() {
		health--;

		UIGameObject.SendMessage("UpdateBossHealth", health * 2 * 10);

		if (health == 0) {
			Ded();
		}
	}

	private void Ded() {
		UIGameObject.SendMessage("DisplayBossBar", false);
		
		Destroy(GetComponent<BoxCollider2D>());
		Destroy(GetComponent<Rigidbody2D>());
		
		Component[] allComponents = GetComponents(typeof(MonoBehaviour));
		foreach (Component gameObjectComponent in allComponents) Destroy((MonoBehaviour)gameObjectComponent);
		
		gameManager.SendMessage("AddMonies", 20);
		
		// asteroidSpawner.SendMessage("CanSpawn", true); //TODO: spawn asteroids again. after shop?
	}
}
