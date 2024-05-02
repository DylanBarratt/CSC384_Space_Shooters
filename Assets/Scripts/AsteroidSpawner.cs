using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour {
	[SerializeField] private GameObject asteriod;
	
	private float asteroidInterval;
	private int screenWidth = 720;

	private float xPos = 0f;
	private float yPos;
	private float ySpawnRange = 1.5f;
	private float xSpawnRange = 2.3f;

	private bool shouldSpawn = true;

	private void Start() {
		asteroidInterval = 1.5f;

		yPos = transform.position.y;
		xSpawnRange = Camera.main.ScreenToWorldPoint(new Vector2(screenWidth, 0)).x;
		
		SpawnAsteroids();
	}

	private void SpawnAsteroids() {
		if (!shouldSpawn) return;
		
		Vector2 spawnPos = new Vector2(
			0 + Random.Range(-xSpawnRange, xSpawnRange), 
			yPos + Random.Range(-ySpawnRange, ySpawnRange)
			);
		
		GameObject a = Instantiate(asteriod, spawnPos, quaternion.identity);
		a.GetComponent<Asteroid>().Init(1f, 3);
		
		Invoke(nameof(SpawnAsteroids), asteroidInterval);
	}

	private void CanSpawn(bool spawn) {
		shouldSpawn = spawn;

		if (shouldSpawn) {
			SpawnAsteroids();
		}
		else {
			DestroyAllAsteroids();
		}
	}

	private void DestroyAllAsteroids() {
		GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

		// Loop through each asteroid and destroy it
		foreach (GameObject asteroid in asteroids)
		{
			asteroid.SendMessage("KillAsteroid", false);
		}
	}

	//TODO: FOR TESTING ONLY!!
	private void ShowBounds() {
		Instantiate(asteriod, new Vector2(xPos - xSpawnRange, yPos + ySpawnRange), quaternion.identity); //Left Top
		Instantiate(asteriod, new Vector2(xPos - xSpawnRange, yPos - ySpawnRange), quaternion.identity); //Left Bot
		
		Instantiate(asteriod, new Vector2(xPos + xSpawnRange, yPos + ySpawnRange), quaternion.identity); //Right Top
		Instantiate(asteriod, new Vector2(xPos + xSpawnRange, yPos - ySpawnRange), quaternion.identity); //Right Bot
		Debug.Break();	
	}
}
