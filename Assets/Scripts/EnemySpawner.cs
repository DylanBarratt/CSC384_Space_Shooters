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
	
	private void Spawn() {
		if (enemiesSpawned >= maxEnemies) {
			return;
		}
		
		for (int i = 0; i < numEnemies.Length; i++) {
			if (numEnemies[i] > 0) {

				if (i == 3 && e4Count >= 1) {
					break; //dont spawn more than one e4
				} 
				
				SpawnIndex(i);
				break; //once an enemy is spawned stop spawning
			}
		}

		FinishedSpawnsCheck();
	}

	private void SpawnIndex(int index) {
		List<Transform> availableSpawnLocs = new List<Transform>(spawnPoints);
		availableSpawnLocs.Remove(lastSpawnLoc);
		Transform spawnLoc = RandomFromList(availableSpawnLocs);
				
		GameObject e = Instantiate(enemies[index], spawnLoc.position, enemies[index].transform.rotation);
		enemiesSpawned++;
				
		//health, speed, value
		float[] eVals = {enemyStats[index, 0], enemyStats[index, 1], enemyStats[index, 2]};
		e.SendMessage("EnemyInit", eVals);
				
		numEnemies[index]--;
		lastSpawnLoc = spawnLoc;
		
		if (index == 3) {
			e4Count++;
		}
	}

	private void FinishedSpawnsCheck() {
		for (int i = 0; i < numEnemies.Length; i++) {
			if (numEnemies[i] != 0) {
				Invoke(nameof(Spawn), frequency);
				return;
			}
		}
		
		Debug.Log("All enemies spawned yo");
	}

	private Transform RandomFromList(List<Transform> list) {
		return list[Random.Range(0, list.Count)];
	}
}
