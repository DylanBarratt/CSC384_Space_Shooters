using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject EnemySpawner;

    private float enemySpawnDelay = 3f;
    private void Start() {
        Invoke(nameof(startEnemySpawn), enemySpawnDelay);

    }

    private void startEnemySpawn() {
        int[] enemyData = new int[5];
        enemyData[0] = 10; //num enemy 1
        enemyData[1] = 0; //num enemy 2
        enemyData[2] = 0; //num enemy 3
        enemyData[3] = 0; //num enemy 4
		
        enemyData[4] = 4; //spawn delay
        EnemySpawner.SendMessage("EnemySpawnInit", enemyData);
    }
}
