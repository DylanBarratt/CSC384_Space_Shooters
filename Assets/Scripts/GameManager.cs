using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject EnemySpawner;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject Starz;
    
    private int currLvlID;
    
    private LevelData[] LEVEL_SPAWN_AMOUNTS;
    
    private void Start() {
        currLvlID = 0;
        
        SetSpawnAmounts();
        StartLevel();
    }

    private void SetSpawnAmounts() {
        bool ezMode = SaveSystem.LoadSettings().ezMode;
        
        if (!ezMode) { //normal
            LEVEL_SPAWN_AMOUNTS = new LevelData [] {
                new (0,  2f, 5, 3, 0, 0),
                new (1,  1.5f, 3, 5, 3, 0),
                new (2,  1f, 3, 3, 7, 0),
                //new (3,  0.5f, 3, 3, 5, 3), lvl4?
            };
        } else { //easy
            Debug.Log("EASY MODE NOOOOOB");
            LEVEL_SPAWN_AMOUNTS = new LevelData [] {
                new (0,  1, 1, 1, 0, 0 ), 
                new (1,  1, 0, 1, 1, 0 ), 
                new (2,  1, 1, 1, 1, 0 ), 
                //new (3,  1, 1, 1, 1, 1 ), lvl4?
            };
        }
    }
    
    private void NextLevel() {
        currLvlID++;
		
        if (currLvlID >= LEVEL_SPAWN_AMOUNTS.Length) {
            EndGame();
            return;
        }
		
        StartLevel();
    }

    private void StartLevel() {
        LevelData curLvl = LEVEL_SPAWN_AMOUNTS[currLvlID];
        
        EnemySpawner.SendMessage("EnemySpawnInit", curLvl);
        Starz.SendMessage("SetSpeed", ((float)currLvlID / 2 + 1) / 10);
    }

    private void EndGame() {
        Debug.Log(currLvlID);
        SaveSystem.SaveWin();
        SceneManager.LoadScene("Menu");
    }
}
