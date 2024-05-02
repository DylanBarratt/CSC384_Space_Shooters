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
	private int enemiesAlive;
	private int maxEnemies;

	private LevelData lvlData;

	private Transform lastSpawnLoc;
	
	public void EnemyDestroyed(int eValue) { 
		enemiesAlive--;
	}

	public void EnemySpawnInit(LevelData spawnAmount) {
		lvlData = new LevelData(spawnAmount.lvlId, spawnAmount.spawnDelay, spawnAmount.e1, spawnAmount.e2, spawnAmount.e3, spawnAmount.e4);
		
		numEnemies[0] = lvlData.e1;
		numEnemies[1] = lvlData.e2;
		numEnemies[2] = lvlData.e3;
		numEnemies[3] = lvlData.e4;
		
		lastSpawnLoc = spawnPoints[0];
		enemiesAlive = 0;
		maxEnemies = 3;
		
		Spawn();
	}
	
	//spawn all types of an enemy before moving to the next
	//once all enemies spawned call boss!!
	private void Spawn() {
		Invoke(nameof(Spawn), lvlData.spawnDelay);
		
		for (int i = 0; i < numEnemies.Length; i++) {
			if (CanSpawn(i)) {
				SpawnIndex(i);
				return;
			}
		}

		if (AllSpawned() && enemiesAlive == 0) {
			CancelInvoke();
			Debug.Log("Boss time baby");
			Debug.Log(lvlData.lvlId);
			bossSpawner.SendMessage("SpawnBoss", lvlData.lvlId);
			// NextLevel(); //TODO: should be called after boss has died instead
		}
	}

	private bool CanSpawn(int index) {
		//Too many man
		if (enemiesAlive >= maxEnemies) {
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
		
		e.SendMessage("EnemyInit");
				
		numEnemies[index]--;
		
		lastSpawnLoc = spawnLoc;
		
		enemiesAlive++;
	}

	private Transform RandomFromList(List<Transform> list) {
		return list[Random.Range(0, list.Count)];
	}
}
