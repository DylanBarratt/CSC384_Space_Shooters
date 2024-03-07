using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitShop : MonoBehaviour {
	private GameObject gameManager;

	private void Start() {
		gameManager = GameObject.Find("GameManager");
	}

	private void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.CompareTag("Player")) {
			gameManager.SendMessage("CloseShop");
			gameManager.SendMessage("NextLevel");
		}
	}
}
