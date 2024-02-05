using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private const float BULLET_LIFE = 2.5f;
    private const float BULLET_FORCE = 20f;

    private Rigidbody2D rb;
    
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        Shoot();
    }
    
    private void Shoot() {
        rb.AddForce(Vector3.up * BULLET_FORCE, ForceMode2D.Impulse);
        Destroy(gameObject, BULLET_LIFE);
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet")) {
            return;
        }
        
        Destroy(gameObject);
    }
}
