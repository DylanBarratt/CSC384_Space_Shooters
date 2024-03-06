using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROFUp : MonoBehaviour {
	private GameObject player;

	private int itemValue;
	
	private void Start() {
		player = GameObject.Find("Player");
	}

	private void SetValue(int amnt) {
		itemValue = -(amnt / 10);
	}

	private void ActivateItem() {
		player.SendMessage("AddROF", itemValue);
		Destroy(gameObject);
	}
}
