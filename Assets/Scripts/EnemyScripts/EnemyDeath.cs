using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {
	private GameObject gameManager;
	private GameObject enemySpawner;

	private Animator anime;

	private int enemyValue;

	private void SetValue(int value) {
		enemyValue = value;
	}

	private void Start() {
		gameManager = GameObject.Find("GameManager");
		enemySpawner = GameObject.Find("GameManager/EnemySpawner");

		anime = GetComponent<Animator>();
	}

	private void KillEnemy() {
		if (enemySpawner != null) {
			enemySpawner.SendMessage("EnemyDestroyed", enemyValue);
		}

		if (gameManager != null) {
			gameManager.SendMessage("AddMonies", enemyValue);
		}
		
		gameObject.SendMessage("StopMoving"); //should also stop any shooting calls
		
		anime.SetBool("ded", true);
		
		Destroy(gameObject, 1f);
	}
}
