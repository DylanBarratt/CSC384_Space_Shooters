using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini : MonoBehaviour {
	private GameObject UIGameObject, mainBoss;

	private void Start() {
		UIGameObject = GameObject.Find("GameManager/HUD_UI");
		mainBoss = GameObject.FindGameObjectWithTag("B3");
	}
	
	private void Ded(int[] vals) {
		UIGameObject.SendMessage("DisplayBossBar", false);
		mainBoss.SendMessage("PhaseDed");
		Destroy(gameObject);
	}

	private void UpdateHealthBar(float[] healthVals) {
		UIGameObject.SendMessage("UpdateBossHealth", (healthVals[1] / healthVals[0]) * 100);
	}
}
