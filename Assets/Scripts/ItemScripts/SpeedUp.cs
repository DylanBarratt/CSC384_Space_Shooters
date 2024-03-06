using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour {
	private GameObject player;

	private int itemValue;
	
	private void Start() {
		player = GameObject.Find("Player");
	}

	private void SetValue(int amnt) {
		itemValue = amnt;
	}

	private void ActivateItem() {
		player.SendMessage("AddSpeed", itemValue);
		Destroy(gameObject);
	}
}
