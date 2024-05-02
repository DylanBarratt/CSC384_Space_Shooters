using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOne : MonoBehaviour {

	private int value = 20;
	private float health = 5; //number of minis
	private float startingHealth;

	
	private void Start() {
		startingHealth = health;
	}

	private void MiniKilled() {
		health--;
		
		SendMessage("UpdateHealthBar", new float[] {startingHealth, health}); 
		
		if (health == 0) {
			SendMessage("Ded", new int[] {0, value});
		}
	}

	

	
}
