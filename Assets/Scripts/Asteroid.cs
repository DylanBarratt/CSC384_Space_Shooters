using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    private Rigidbody2D rb;
    
    private float fallingSpeed; //speed that the asteroid starts falling at
    private float rateOfFalling; //rate the asteroid increases speed
    private int health; //number of hits that can be sustained

    public void Init(float fs, float ros, int h) {
        fallingSpeed = fs;
        rateOfFalling = ros;
        health = h;
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        switch(collision.gameObject.tag) {
            case "Boundary":
                Despawn();
                break;
            case "Bullet":
                Damage();
                break;
        }
        
    }

    private void Despawn() {
        Destroy(gameObject);
    }

    private void Damage() {
        health--;
        
        if (health <= 0) {
            Despawn();
        }
    }

    private void FixedUpdate() {
        fallingSpeed += rateOfFalling * Time.deltaTime;
        rb.velocity = Vector2.down * fallingSpeed;
    }
}