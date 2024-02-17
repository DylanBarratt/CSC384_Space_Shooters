using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour {
	[SerializeField] private GameObject[] enemies;
	[SerializeField] private Transform[] spawnPoints;

	private List<int> noEmptyNumEnemies;
	private int[] numEnemies = new int[4];
	private int frequency;
	private int enemiesSpawned;
	private int e4Count;
	private int maxEnemies;

	//health, speed, value
	private float[,] enemyStats = {
		{3, 1.5f, 1}, //e1
		{5, 1f, 2}, //e2
		{2, 4f, 3}, //e3
		{3, 8, 4}, //e4 TODO: maybe this could match the players health!?!
	};

	private Transform lastSpawnLoc;
	
	public void EnemyDestroyed(int eValue) { 
		//felt like this was a weird (probably non-optimal) but slightly clever way of determining if the enemy killed was e4 :) - Dylan
		if (eValue == enemyStats[3, 2]) { 
			e4Count--;
		}
		
		enemiesSpawned--;
		Invoke(nameof(Spawn), frequency);
	}

	public void EnemySpawnInit(int[] spawnAmount) {
		if (spawnAmount.Length < 5) {
			Debug.LogError("Enemy spawn data too short...");
		}
		
		numEnemies[0] = spawnAmount[0];
		numEnemies[1] = spawnAmount[1];
		numEnemies[2] = spawnAmount[2];
		numEnemies[3] = spawnAmount[3];
		
		frequency = spawnAmount[4];
		
		lastSpawnLoc = spawnPoints[0];
		enemiesSpawned = 0;
		e4Count = 0;
		maxEnemies = 3;
		Invoke(nameof(Spawn), frequency);
	}
	
	//spawn all types of an enemy before moving to the next
	//once all enemies spawned call boss!!
	private void Spawn() {
		for (int i = 0; i < numEnemies.Length; i++) {
			if (CanSpawn(i)) {
				SpawnIndex(i);
				Invoke(nameof(Spawn), frequency);
				return;
			}
		}

		if (AllSpawned()) {
			CancelInvoke();
			Debug.Log("Boss time baby");
		}
	}

	private bool CanSpawn(int index) {
		//Too many man
		if (enemiesSpawned >= maxEnemies) {
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
		enemiesSpawned++;
	}

	private Transform RandomFromList(List<Transform> list) {
		return list[Random.Range(0, list.Count)];
	}
}
