using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject EnemySpawner;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject Starz;

    
    private int NUM_LEVELS = 4;
    private int lvlID;
    
    private void Start() {
        lvlID = 1;
        //TODO: check if tutorial has been done, if not load tut instead
        StartLevel();
    }

    //0 tut, lvls 1 - 4
    // private int[,] LEVEL_SPAWN_AMOUNTS = {
    //     //spawn_delay, e1, e2, e3, e4
    //     { 1, 1, 0, 0, 0 }, 
    //     { 4, 10, 3, 0, 0},
    //     { 3, 5, 5, 3, 0},
    //     { 2, 3, 3, 15, 0},
    //     { 1, 3, 3, 5, 3},
    // };
    
    //TODO: delete dev vals
    private int[,] LEVEL_SPAWN_AMOUNTS = {
        //spawn_delay, e1, e2, e3, e4
        { 1, 1, 0, 0, 0 }, 
        { 1, 1, 0, 0, 0 }, 
        { 1, 1, 0, 0, 0 }, 
        { 1, 1, 0, 0, 0 }, 
        { 1, 1, 0, 0, 0 }, 
    };
    
    private void NextLevel() {
        lvlID++;
        Debug.Log("Moving to level " + lvlID);
		
        if (lvlID > NUM_LEVELS) {
            EndGame();
            return;
        }
		
        StartLevel();
    }

    private void StartLevel() {
        int[] curLvl = {
            lvlID,
            LEVEL_SPAWN_AMOUNTS[lvlID, 0], 
            LEVEL_SPAWN_AMOUNTS[lvlID, 1], 
            LEVEL_SPAWN_AMOUNTS[lvlID, 2], 
            LEVEL_SPAWN_AMOUNTS[lvlID, 3], 
            LEVEL_SPAWN_AMOUNTS[lvlID, 4]
        };
        
        EnemySpawner.SendMessage("EnemySpawnInit", curLvl);
        Starz.SendMessage("SetSpeed", ((float)lvlID / 2 + 1) / 10);
    }

    private void EndGame() {
        //TODO: maybe save stats here?
        Debug.Log("GGs bro"); //TODO: implement end game here
        SceneManager.LoadScene("Menu");
    }
}
