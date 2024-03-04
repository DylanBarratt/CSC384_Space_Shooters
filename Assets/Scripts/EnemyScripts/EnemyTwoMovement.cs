using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyTwoMovement : MonoBehaviour {
    //TODO: lots of very similar code to enemy one movement...
    private Rigidbody2D rb;    
    private Animator anime;
    
    private float speed;
    
    private float moveDuration = 3f;
    

    private void Start() {
        anime = GetComponent<Animator>();
    }

    private void YReached(float s) {
        speed = s;
        rb = GetComponent<Rigidbody2D>();
        MoveThenStop();
    }

    //idk i thought these method names were funny :D - Dylan
    //move for moveDuration 
    private void MoveThenStop() {
        rb.velocity = new Vector2(speed, 0);
        Invoke(nameof(StopThenShoot), moveDuration);
    }

    //stop for half move duration
    private void StopThenShoot() {
        rb.velocity = Vector2.zero;
        anime.SetBool("Stopped", true);
        Invoke(nameof(ShootThenMove), moveDuration / 2);
    }

    //shoot then stop for half duration
    private void ShootThenMove() {
        gameObject.SendMessage("Shoot");
        anime.SetBool("Stopped", false);
        Invoke(nameof(MoveThenStop), moveDuration / 2);
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("SBoundary")) {
            speed = -speed; 
            CancelInvoke(); //stop the invoke running before hitting the boundary
            MoveThenStop();
        }
    }

    private void Ded() {
        CancelInvoke();
    }
}
