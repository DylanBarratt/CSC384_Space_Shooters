using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossThree : MonoBehaviour {
	private GameObject asteroidSpawner;
	private GameObject gameManager;

	[SerializeField] private GameObject b1, b2;

	private int value = 30, phase;
	
	private void Start() {
		gameManager = GameObject.Find("GameManager");
		asteroidSpawner = GameObject.Find("GameManager/AsteroidSpawner");

		asteroidSpawner.SendMessage("CanSpawn", false);
		
		b1.SetActive(true);
		phase = 0;
	}

	private void PhaseDed() {
		if (phase == 0) {
			b2.SetActive(true);
			phase++;
			return;
		} 
		
		if (phase == 1) {
			gameManager.SendMessage("AddMonies", 2);
			gameManager.SendMessage("OpenShop", value);
		}
	}
}
