using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour {
	[SerializeField] private GameObject[] enemies;
	[SerializeField] private Transform[] spawnPoints;
	
	private int[] numEnemies = new int[4];
	private int frequency;
	private int enemiesSpawned;
	private int maxEnemies;

	//health, speed, value
	private float[,] enemyStats = {
		{4, 1.5f, 1}, //e1
		{3, 1.5f, 1}, //e2
		{3, 1.5f, 1}, //e3
		{3, 1.5f, 1}, //e4
	};

	private Transform lastSpawnLoc;

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
		maxEnemies = 3;
		Invoke(nameof(Spawn), frequency);
	}

	public void EnemyDestroyed() {
		enemiesSpawned--;
	}

	private void Spawn() {
		if (enemiesSpawned >= maxEnemies) {
			return;
		}
		
		for (int i = 0; i < numEnemies.Length; i++) {
			if (numEnemies[i] > 0) {
				List<Transform> availableSpawnLocs = new List<Transform>(spawnPoints);
				availableSpawnLocs.Remove(lastSpawnLoc);
				Transform spawnLoc = RandomFromList(availableSpawnLocs);
				lastSpawnLoc = spawnLoc;
				
				GameObject e = Instantiate(enemies[i], spawnLoc.position, enemies[i].transform.rotation);
				enemiesSpawned++;
				
				//health, speed, value
				float[] eVals = {enemyStats[i, 0], enemyStats[i, 1], enemyStats[i, 2]};
				e.SendMessage("EnemyInit", eVals);
				
				numEnemies[i]--;
			}
		}

		FinishedSpawnsCheck();
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
