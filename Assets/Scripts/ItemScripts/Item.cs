using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour {
	[SerializeField] private TextMeshPro priceText;
	private GameObject gameManager;

	private int itemID;
	private int itemCost;
	private int itemValue;

	private void InitItem(int[] vals) {
		itemID = vals[0];
		itemCost = vals[1];
		itemValue = vals[2];
		
		gameObject.SendMessage("SetValue", itemValue);
		
		priceText.SetText("$" + itemCost);
	}
	
	private void Start() {
		gameManager = GameObject.Find("GameManager");
	}

	private void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.CompareTag("Player")) {
			gameManager.SendMessage("BuyItem", new [] {itemID, itemCost});
		}
	}
}
