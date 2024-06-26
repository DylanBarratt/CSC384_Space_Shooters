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

    private bool ded;

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
                KillAsteroid(true);
                break;
            case "Boundary":
                Invoke(nameof(DestroyOnBoundary), 2f); 
                break;
        }
    }

    private void Damage() {
        health--;
        
        if (health <= 0) {
            KillAsteroid(true);
        }
    }

    private void FixedUpdate() {
        if (ded) return;
        
        fallingSpeed += Time.deltaTime;
        rb.velocity = Vector2.down * fallingSpeed;
    }
    
    private void DestroyOnBoundary() {
        Destroy(gameObject);
    }
    
    //with bool does animation
    private void KillAsteroid(bool player) {
        DedAnime();

        if (player) {
            gameManager.SendMessage("AddMonies", moniesValue);
        }
    }

    private void DedAnime() {
        GetComponent<Animator>().SetBool("Ded", true);
        ded = true;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Collider2D>());
            
        Destroy(gameObject, 1f);
    }
}
