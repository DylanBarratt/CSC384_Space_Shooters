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
		UIGameObject = GameObject.Find("HUD_UI");
		asteroidSpawner = GameObject.Find("GameManager/AsteroidSpawner");

		asteroidSpawner.SendMessage("CanSpawn", false);
	}
	
	private int[] vals;
	//lvl, value
	private void Ded(int[] vals) {
		this.vals = vals;
		UIGameObject.SendMessage("DisplayBossBar", false);
		
		Invoke(nameof(Next), 1.5f);
	}

	private void Next() {
		Destroy(GetComponent<BoxCollider2D>());
		Destroy(GetComponent<Rigidbody2D>());
		
		Component[] allComponents = GetComponents(typeof(MonoBehaviour));
		foreach (Component gameObjectComponent in allComponents) Destroy((MonoBehaviour)gameObjectComponent);
		
		gameManager.SendMessage("AddMonies", vals[1]);
		
		gameManager.SendMessage("OpenShop", vals[0]);
		Destroy(gameObject);
	}
	
	//startingHealth, health
	private void UpdateHealthBar(float[] healthVals) {
		UIGameObject.SendMessage("UpdateBossHealth", (healthVals[1] / healthVals[0]) * 100);
	}
}
