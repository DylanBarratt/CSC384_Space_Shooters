using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour {
	private GameObject player;
	
	private VisualElement root;
	private Label monies;
	private ProgressBar health;
	private ProgressBar bossHealth;
	private Label healthTxt;
	
	private void Start() {
		root = gameObject.GetComponent<UIDocument>().rootVisualElement;
		monies = root.Q<Label>("MoniesCounter");
		health = root.Q<ProgressBar>("HealthBar");
		bossHealth = root.Q<ProgressBar>("BossBar");
		healthTxt = root.Q<Label>("HealthBarTxt");
		player = GameObject.Find("Player");
		
		player.SendMessage("UpdateHealth"); //load correct value
	}
	
	private void UpdateHealth(float val) {
		health.value = val;
		healthTxt.text = (int) val + "%";
	}

	private void UpdateBossHealth(float val) {
		bossHealth.value = val;
	}

	private void DisplayBossBar(bool val) {
		bossHealth.visible = val;
	}

	private void UpdateMonies(int val) {
		//TODO: could put something funny here for when money over 999
		monies.text = "$" + val;
	}
}
