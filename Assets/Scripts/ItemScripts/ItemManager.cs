using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemManager : MonoBehaviour {
	[SerializeField] private GameObject HUD;
	private GameObject asteroidSpawner;
	
	[SerializeField] private GameObject healthUpPrefab;
	[SerializeField] private GameObject speedUpPrefab;
	[SerializeField] private GameObject rofUpPrefab;
	
	private List<GameObject> items  = new List<GameObject>();
	
	private int loopMonies = 0;

	private void Start() {
		asteroidSpawner = GameObject.Find("GameManager/AsteroidSpawner");
	}

	private void OpenShop() {
		SpawnItems();
		Debug.Log("Hello sirt");
	}

	private void CloseShop() {
		DespawnItems();
		asteroidSpawner.SendMessage("CanSpawn", true);
		//TODO: start next level as well
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
		instance.SendMessage("InitItem", new [] {2, 10, 1});
		items.Add(instance);
	}

	private void DespawnItems() {
		foreach (GameObject item in items) {
			Destroy(item);
		}
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
