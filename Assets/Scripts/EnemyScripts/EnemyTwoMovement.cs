using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyTwoMovement : MonoBehaviour {
    //TODO: lots of very similar code to enemy one movement...
    
    private Rigidbody2D rb;
    
    private float speed;
    
    private float moveDuration = 3f;

    private void YReached(float s) {
        speed = s;
        rb = GetComponent<Rigidbody2D>();
        MoveThenStop();
    }

    //idk i thought these method names were funny :D - Dylan
    private void MoveThenStop() {
        rb.velocity = new Vector2(speed, 0);
        Invoke(nameof(StopThenShoot), moveDuration);
    }

    private void StopThenShoot() {
        rb.velocity = Vector2.zero;
        Invoke(nameof(ShootThenMove), moveDuration / 2);
    }

    private void ShootThenMove() {
        gameObject.SendMessage("Shoot");
        Invoke(nameof(MoveThenStop), moveDuration / 2);
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("SBoundary")) {
            speed = -speed; 
            MoveThenStop();
        }
    }
}
