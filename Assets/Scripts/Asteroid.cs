using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    private GameObject gameManager;

    private Rigidbody2D rb;
    
    private float fallingSpeed; //speed that the asteroid starts falling at
    private int health; //number of hits that can be sustained
    private int moniesValue = 1;

    public void Init(float fs, int h) {
        fallingSpeed = fs;
        health = h;
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch(collision.gameObject.tag) {
            case "PlayerBullet":
                Damage();
                break;
            case "Player":
                KillAsteroid();
                break;
            case "Boundary":
                Invoke(nameof(KillAsteroid), 2f); 
                break;
        }
    }

    //TODO: could take a parameter to know what kills it?
    private void KillAsteroid() {
        Destroy(gameObject);
        gameManager.SendMessage("AddMonies", moniesValue);
    }

    private void Damage() {
        health--;
        
        if (health <= 0) {
            KillAsteroid();
        }
    }

    private void FixedUpdate() {
        fallingSpeed += Time.deltaTime;
        rb.velocity = Vector2.down * fallingSpeed;
    }
}
