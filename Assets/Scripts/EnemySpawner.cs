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

	private Transform lastSpawnLoc;

	public void EnemySpawnInit(int[] enemyData) {
		if (enemyData.Length < 5) {
			Debug.LogError("Enemy spawn data too short...");
		}
		
		numEnemies[0] = enemyData[0];
		numEnemies[1] = enemyData[1];
		numEnemies[2] = enemyData[2];
		numEnemies[3] = enemyData[3];
		
		frequency = enemyData[4];
		lastSpawnLoc = spawnPoints[0];
		Invoke(nameof(Spawn), frequency);
	}

	private void Spawn() {
		
		
		for (int i = 0; i < numEnemies.Length; i++) {
			if (numEnemies[i] > 0) {
				List<Transform> availableSpawnLocs = new List<Transform>(spawnPoints);
				availableSpawnLocs.Remove(lastSpawnLoc);
				Transform spawnLoc = RandomFromList(availableSpawnLocs);
				lastSpawnLoc = spawnLoc;
				
				GameObject e = Instantiate(enemies[i], spawnLoc.position, enemies[i].transform.rotation);
				
				float[] eVals = {2, 3};
				e.SendMessage("Init", eVals);
				
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
			
			Debug.Log("All enemies spawned yo");
		}
	}

	private Transform RandomFromList(List<Transform> list) {
		return list[Random.Range(0, list.Count)];
	}
}
