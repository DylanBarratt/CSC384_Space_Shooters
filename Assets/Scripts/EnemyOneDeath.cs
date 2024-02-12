using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneDeath : MonoBehaviour {
	private GameObject gameManager;
	private GameObject enemySpawner;

	private int enemyValue;

	private void SetValue(int value) {
		enemyValue = value;
	}

	private void Start() {
		gameManager = GameObject.Find("GameManager");
		enemySpawner = GameObject.Find("GameManager/EnemySpawner");
	}

	private void KillEnemy() {
		Destroy(gameObject);

		if (enemySpawner != null) {
			enemySpawner.SendMessage("EnemyDestroyed");
		}

		if (gameManager != null) {
			gameManager.SendMessage("AddMonies", enemyValue);
		}
	}
}
