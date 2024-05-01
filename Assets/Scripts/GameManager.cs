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
    
    private int lvlID;
    
    private LevelData[] LEVEL_SPAWN_AMOUNTS;
    
    private void Start() {
        lvlID = 0;
        
        SetSpawnAmounts();
        
        //TODO: check if tutorial has been done, if not load tut instead
        StartLevel();
    }

    private void SetSpawnAmounts() {
        bool ezMode = SaveSystem.LoadSettings().ezMode;
        
        if (!ezMode) { //normal
            LEVEL_SPAWN_AMOUNTS = new LevelData [] {
                new (0,  2f, 5, 3, 0, 0),
                new (1,  1.5f, 3, 5, 3, 0),
                new (2,  1f, 3, 3, 7, 0),
                new (3,  0.5f, 3, 3, 5, 3),
            };
        } else { //easy
            Debug.Log("EASY MODE NOOOOOB");
            LEVEL_SPAWN_AMOUNTS = new LevelData [] {
                new (0,  1, 1, 1, 0, 0 ), 
                new (1,  1, 0, 1, 1, 0 ), 
                new (2,  1, 1, 1, 1, 0 ), 
                new (3,  1, 1, 1, 1, 1 ), 
            };
        }
    }
    
    private void NextLevel() {
        lvlID++;
        Debug.Log("Moving to level " + lvlID);
        Debug.Log(LEVEL_SPAWN_AMOUNTS.Length);
		
        if (lvlID == LEVEL_SPAWN_AMOUNTS.Length) {
            EndGame();
            return;
        }
		
        StartLevel();
    }

    private void StartLevel() {
        LevelData curLvl = LEVEL_SPAWN_AMOUNTS[lvlID];
        
        EnemySpawner.SendMessage("EnemySpawnInit", curLvl);
        Starz.SendMessage("SetSpeed", ((float)lvlID / 2 + 1) / 10);
    }

    private void EndGame() {
        //TODO: maybe save stats here?
        Debug.Log("GGs bro"); //TODO: implement end game here
        SceneManager.LoadScene("Menu");
    }
}
