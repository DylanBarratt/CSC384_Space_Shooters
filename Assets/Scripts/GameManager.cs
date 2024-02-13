using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject EnemySpawner;
    [SerializeField] private GameObject HUD;
    
    private float enemySpawnDelay = 3f;

    private int loopMonies;

    public void AddMonies(int amnt) {
        loopMonies += amnt;
        
        HUD.SendMessage("UpdateMonies", loopMonies);
    }

    private void Start() {
        loopMonies = 0;
        
        Invoke(nameof(startEnemySpawn), enemySpawnDelay);
    }

    private void startEnemySpawn() {
        //TODO: this should be done level by level
        int[] enemySpawnAmnt = new int[5];
        enemySpawnAmnt[0] = 10; //num enemy 1
        enemySpawnAmnt[1] = 0; //num enemy 2
        enemySpawnAmnt[2] = 0; //num enemy 3
        enemySpawnAmnt[3] = 0; //num enemy 4
		
        enemySpawnAmnt[4] = 4; //spawn delay
        EnemySpawner.SendMessage("EnemySpawnInit", enemySpawnAmnt);
    }
}
