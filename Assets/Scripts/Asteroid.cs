using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    private Rigidbody2D rb;
    
    private float fallingSpeed; //speed that the asteroid starts falling at
    private int health; //number of hits that can be sustained

    public void Init(float fs, int h) {
        fallingSpeed = fs;
        health = h;
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
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

    //could take a parameter to know what kills it?
    public void KillAsteroid() {
        Destroy(gameObject);
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
