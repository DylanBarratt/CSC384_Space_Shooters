using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour {
	[SerializeField] private TextMeshPro priceText;
	private GameObject gameManager;

	private ItemData data;

	private void InitItem(ItemData vals) {
		data = new ItemData(vals.id, vals.cost, vals.value);
		
		gameObject.SendMessage("SetValue", data.value);
		
		priceText.SetText("$" + data.cost);
	}
	
	private void Start() {
		gameManager = GameObject.Find("GameManager");
	}

	private void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.CompareTag("Player")) {
			gameManager.SendMessage("BuyItem", data);
		}
	}
}
