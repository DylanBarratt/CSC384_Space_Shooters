using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject EnemySpawner;
    
    private void Start() {
        int[] enemyData = new int[5];
        enemyData[0] = 3;
        enemyData[1] = 0;
        enemyData[2] = 0;
        enemyData[3] = 0;
		
        enemyData[4] = 1;
        EnemySpawner.SendMessage("EnemySpawnInit", enemyData);
    }
}
