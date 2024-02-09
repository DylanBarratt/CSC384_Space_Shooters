using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour {
	[SerializeField] private GameObject asteriod;
	
	private int asteroidInterval;

	private float xPos = 0f;
	private float yPos = 7f;
	private float ySpawnRange = 1.5f;
	private float xSpawnRange = 2.3f;
	private int screenWidth = 720;


	private void Start() {
		asteroidInterval = 5;

		xSpawnRange = Camera.main.ScreenToWorldPoint(new Vector2(screenWidth, 0)).x;
		
		SpawnAsteroids();
	}

	private void SpawnAsteroids() {
		Vector2 spawnPos = new Vector2(
			0 + Random.Range(-xSpawnRange, xSpawnRange), 
			yPos + Random.Range(-ySpawnRange, ySpawnRange)
			);
		
		Debug.Log(spawnPos);
		
		GameObject a = Instantiate(asteriod, spawnPos, quaternion.identity);
		a.GetComponent<Asteroid>().Init(1f, 0.2f, 3);
		
		Invoke(nameof(SpawnAsteroids), asteroidInterval);
	}

	//FOR TESTING ONLY!!
	private void ShowBounds() {
		Instantiate(asteriod, new Vector2(xPos - xSpawnRange, yPos + ySpawnRange), quaternion.identity); //Left Top
		Instantiate(asteriod, new Vector2(xPos - xSpawnRange, yPos - ySpawnRange), quaternion.identity); //Left Bot
		
		Instantiate(asteriod, new Vector2(xPos + xSpawnRange, yPos + ySpawnRange), quaternion.identity); //Right Top
		Instantiate(asteriod, new Vector2(xPos + xSpawnRange, yPos - ySpawnRange), quaternion.identity); //Right Bot
		Debug.Break();	
	}
}
