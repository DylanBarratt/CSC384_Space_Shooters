using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemManager : MonoBehaviour {
	[SerializeField] private GameObject HUD;
	private GameObject asteroidSpawner;
	private GameObject player;
	
	
	private List<GameObject> items  = new List<GameObject>();

	private const float SHOP_DELAY = 1f;
	
	private int loopMonies = 0;

	private ItemData[] itemVals = new ItemData[3];
	private void Start() {
		asteroidSpawner = GameObject.Find("GameManager/AsteroidSpawner");
		player = GameObject.Find("Player");
		SetItemVals(1);
	}

	private void SetItemVals(int lvl) {
		//health
		itemVals[0] = new ItemData(0,20, 1);
		
		//speed
		itemVals[1] = new ItemData(1, 30, 2);
		
		//rof
		itemVals[2] = new ItemData(2, 100 / lvl, 1);
	}

	private void OpenShop(int lvl) {
		SetItemVals(lvl + 1);
		
		
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

	
	[SerializeField] private GameObject[] prefabs;

	private void SpawnItems() {
		GameObject instance;
		for (int i = 0; i < itemVals.Length; i++) {
			instance = Instantiate(prefabs[i]);
			Debug.Log(itemVals[i].id);
			instance.SendMessage("InitItem", itemVals[i]);  
			items.Add(instance);
		}

		instance = Instantiate(prefabs[prefabs.Length-1]);
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
	
	private void BuyItem(ItemData vals) {
		if (vals.cost > loopMonies) {
			Debug.Log("Poor");
			return;
		}

		AddMonies(-vals.cost);
		
		items[vals.id].SendMessage("ActivateItem");
	}
}
