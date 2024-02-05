using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    private Rigidbody2D rb;
    
    private float fallingSpeed;
    [SerializeField] private float rateOfFalling;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        
        fallingSpeed = 2f;
        rateOfFalling = 0.2f;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Boundary")) {
            Despawn();
        }
    }

    private void Despawn() {
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        fallingSpeed += rateOfFalling * Time.deltaTime;
        rb.velocity = Vector2.down * fallingSpeed;
    }
}
