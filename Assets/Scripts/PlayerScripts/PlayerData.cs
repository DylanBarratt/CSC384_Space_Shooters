using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData  {
	public int health;
	public float speed;
	public float rateOfFire;

	public PlayerData(int health, float speed, float rateOfFire) {
		this.health = health;
		this.speed = speed;
		this.rateOfFire = rateOfFire;
	}
}
