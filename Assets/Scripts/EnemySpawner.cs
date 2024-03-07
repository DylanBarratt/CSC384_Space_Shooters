using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour {
	[SerializeField] private GameObject[] enemies;
	[SerializeField] private Transform[] spawnPoints;
	[SerializeField] private GameObject gameManager;
	[SerializeField] private GameObject bossSpawner;
	
	private List<int> noEmptyNumEnemies;
	private int[] numEnemies = new int[4];
	private int frequency;
	private int enemiesAlive;
	private int e4Count;
	private int maxEnemies;
	private int lvlID;

	//health, speed, value
	// private float[,] enemyStats = {
	// 	{3, 1.5f, 1}, //e1
	// 	{5, 1f, 2}, //e2
	// 	{2, 4f, 3}, //e3
	// 	{3, 8, 4}, //e4 TODO: maybe this could match the players health!?!
	// };
	//
	//TODO: delete testing vals!!!!!
	private float[,] enemyStats = {
		{1, 1.5f, 1}, //e1
		{1, 1f, 2}, //e2
		{1, 4f, 3}, //e3
		{1, 8, 4}, //e4
	};

	private Transform lastSpawnLoc;
	
	public void EnemyDestroyed(int eValue) { 
		//felt like this was a weird (probably non-optimal) but slightly clever way of determining if the enemy killed was e4 :) - Dylan
		if (eValue == enemyStats[3, 2]) { 
			e4Count--;
		}
		
		enemiesAlive--;
	}

	public void EnemySpawnInit(int[] spawnAmount) {
		if (spawnAmount.Length < 5) {
			Debug.LogError("Enemy spawn data too short...");
		}

		lvlID = spawnAmount[0];
		frequency = spawnAmount[1];
		
		numEnemies[0] = spawnAmount[2];
		numEnemies[1] = spawnAmount[3];
		numEnemies[2] = spawnAmount[4];
		numEnemies[3] = spawnAmount[5];
		
		lastSpawnLoc = spawnPoints[0];
		enemiesAlive = 0;
		e4Count = 0;
		maxEnemies = 3;
		
		Spawn();
	}
	
	//spawn all types of an enemy before moving to the next
	//once all enemies spawned call boss!!
	private void Spawn() {
		Invoke(nameof(Spawn), frequency);
		
		for (int i = 0; i < numEnemies.Length; i++) {
			if (CanSpawn(i)) {
				SpawnIndex(i);
				return;
			}
		}

		if (AllSpawned() && enemiesAlive == 0) {
			CancelInvoke();
			Debug.Log("Boss time baby");
			Debug.Log(lvlID);
			bossSpawner.SendMessage("SpawnBoss", lvlID);
			// NextLevel(); //TODO: should be called after boss has died instead
		}
	}

	private bool CanSpawn(int index) {
		//Too many man
		if (enemiesAlive >= maxEnemies) {
			return false;
		}

		//enough 4s
		if (index == 3 && e4Count >= 1) {
			return false;
		}

		//all index spawned
		if (numEnemies[index] <= 0) {
			return false;	
		}
		
		return true;
		// I realise a cleverer person could combine all these ifs into a single return statement. but that person is not me... - Dylan
	}

	private bool AllSpawned() {
		foreach (int num in numEnemies) {
			if (num > 0) {
				return false;
			}
		}

		return true;
	}
	
	private void SpawnIndex(int index) {
		List<Transform> availableSpawnLocs = new List<Transform>(spawnPoints);
		availableSpawnLocs.Remove(lastSpawnLoc);
		Transform spawnLoc = RandomFromList(availableSpawnLocs);
				
		GameObject e = Instantiate(enemies[index], spawnLoc.position, enemies[index].transform.rotation);
							//health, speed, value
		float[] eVals = {enemyStats[index, 0], enemyStats[index, 1], enemyStats[index, 2]};
		e.SendMessage("EnemyInit", eVals);
				
		numEnemies[index]--;
		
		lastSpawnLoc = spawnLoc;
		
		if (index == 3) {
			e4Count++;
		}
		
		enemiesAlive++;
	}

	private Transform RandomFromList(List<Transform> list) {
		return list[Random.Range(0, list.Count)];
	}
}
