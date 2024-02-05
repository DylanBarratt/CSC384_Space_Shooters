using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour {
	[SerializeField] private GameObject asteriod;

	private int numAsteroids;
	private int asteroidInterval;

	private Vector2 ogSpawnLoc;
	private float ySpawnRange;
	private float xSpawnRange;

	private void Start() {
		numAsteroids = 10;
		asteroidInterval = 3;

		ogSpawnLoc = gameObject.transform.position;
		
		ySpawnRange = 5f;
		xSpawnRange = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
		
		Invoke(nameof(SpawnAsteroids), asteroidInterval);
	}

	private void SpawnAsteroids() {
		if (numAsteroids > 0) {
			Vector2 spawnPos = new Vector2(
				ogSpawnLoc.x + Random.Range(-xSpawnRange, xSpawnRange), 
				ogSpawnLoc.y + Random.Range(-ySpawnRange, ySpawnRange)
				);
			
			Instantiate(asteriod, spawnPos, quaternion.identity);
			
			numAsteroids--;
			Invoke(nameof(SpawnAsteroids), asteroidInterval);
		}
	}

	//FOR TESTING ONLY!!
	private void ShowBounds() {
		Instantiate(asteriod, ogSpawnLoc, quaternion.identity); //mid
		
		Instantiate(asteriod, new Vector2(ogSpawnLoc.x - xSpawnRange, ogSpawnLoc.y + ySpawnRange), quaternion.identity); //Left Top
		Instantiate(asteriod, new Vector2(ogSpawnLoc.x - xSpawnRange, ogSpawnLoc.y - ySpawnRange), quaternion.identity); //Left Bot
		
		Instantiate(asteriod, new Vector2(ogSpawnLoc.x + xSpawnRange, ogSpawnLoc.y + ySpawnRange), quaternion.identity); //Right Top
		Instantiate(asteriod, new Vector2(ogSpawnLoc.x + xSpawnRange, ogSpawnLoc.y - ySpawnRange), quaternion.identity); //Right Bot

	}
}
