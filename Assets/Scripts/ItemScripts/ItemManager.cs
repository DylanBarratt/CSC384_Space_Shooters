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

	private int[,] itemVals = new int [3, 3];
	private void Start() {
		asteroidSpawner = GameObject.Find("GameManager/AsteroidSpawner");
		player = GameObject.Find("Player");
		SetItemVals(1);
	}

	private void SetItemVals(int lvl) {
		//health
		itemVals[0,0] =  20 * lvl; //cost
		itemVals[0,1] =  1; //value
		
		//speed
		itemVals[1,0] =  30 * lvl; //cost
		itemVals[1,1] =  2; //value
		
		//rof
		itemVals[2,0] =  100 / lvl; //cost
		itemVals[2,1] =  1; //value
	}

	private void OpenShop(int lvl) {
		SetItemVals(lvl);
		
		
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
		for (int i = 0; i < 3; i++) {
			instance = Instantiate(prefabs[i]);
			//id, cost, value
			instance.SendMessage("InitItem", new [] {i, itemVals[i, 0], itemVals[i, 1]});  //TODO: should this be set based on level?
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
