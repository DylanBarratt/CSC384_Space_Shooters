using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemManager : MonoBehaviour {
	[SerializeField] private GameObject HUD;
	private GameObject asteroidSpawner;
	private GameObject player;
	
	[SerializeField] private GameObject healthUpPrefab;
	[SerializeField] private GameObject speedUpPrefab;
	[SerializeField] private GameObject rofUpPrefab;
	[SerializeField] private GameObject exitShopPrefab;
	
	private List<GameObject> items  = new List<GameObject>();

	private const float SHOP_DELAY = 1f;
	
	private int loopMonies = 0;

	private void Start() {
		asteroidSpawner = GameObject.Find("GameManager/AsteroidSpawner");
		player = GameObject.Find("Player");
	}

	private void OpenShop() {
		items = new List<GameObject>();
		
		if (player != null) {
			Invoke(nameof(ResetPlayer), SHOP_DELAY);
		}
		
		Invoke(nameof(SpawnItems), SHOP_DELAY);
		
		Debug.Log("Hello sirt");
		
	}

	private void ResetPlayer() {
		player.SendMessage("ResetLoc");

	}

	private void CloseShop() {
		DespawnItems();
		
		asteroidSpawner.SendMessage("CanSpawn", true);
		if (player != null) {
			player.SendMessage("ResetLoc");
		}
		
		Debug.Log("Goodbye mein sirt");
	}

	private void SpawnItems() {
		GameObject instance = Instantiate(healthUpPrefab);
		
		//id, cost, value
		instance.SendMessage("InitItem", new [] {0, 10, 1});  //TODO: should this be set based on level?
		items.Add(instance);

		instance = Instantiate(speedUpPrefab);
		instance.SendMessage("InitItem", new [] {1, 10, 2});
		items.Add(instance);

		instance = Instantiate(rofUpPrefab);
		instance.SendMessage("InitItem", new [] {2, 10, 3});
		items.Add(instance);

		instance = Instantiate(exitShopPrefab);
		items.Add(instance);
	}

	private void DespawnItems() {
		foreach (GameObject item in items) {
			Destroy(item);
		}

		items = null;
	}
	
	private void AddMonies(int amnt) {
		loopMonies += amnt;
        
		HUD.SendMessage("UpdateMonies", loopMonies);
	}
	
	private void BuyItem(int[] vals) {
		int itemID = vals[0];
		int cost = vals[1];

		if (cost > loopMonies) {
			Debug.Log("Poor");
			return;
		}

		AddMonies(-cost);
		
		items[itemID].SendMessage("ActivateItem");
	}
}
