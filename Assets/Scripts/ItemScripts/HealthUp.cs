using System;
using UnityEngine;

public class HealthUp : MonoBehaviour {
	private GameObject player;

	private int itemValue;
	
	private void Start() {
		player = GameObject.Find("Player");
	}

	private void SetValue(int amnt) {
		itemValue = amnt;
	}

	private void ActivateItem() {
		player.SendMessage("AddHealth", itemValue);
		Destroy(gameObject);
	}
}
	