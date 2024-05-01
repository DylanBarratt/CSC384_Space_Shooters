using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script with help from: https://stuartspixelgames.com/2018/06/24/simple-2d-top-down-movement-unity-c/
public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private Animator anime;

    private float speed = 4f, horizontal, vertical;

    private bool canHorizontal, canVertical;

    private const float MOVE_LIMITER = 0.7f;
    
    private Vector2 START_POS = new Vector2(0f, -4.5f);
    
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        
        canHorizontal = true;
        canVertical = true;
        
        ResetLoc();
    }

    private void SetSpeed(float val) {
        speed = val;
    }

    private void ToggleHorizontal(bool val) {
        canHorizontal = val;
    }

    private void ToggleVertical(bool val) {
        canVertical = val;
    }
    
    private void Update() {
        if (canHorizontal) {
            horizontal = Input.GetAxisRaw("Horizontal");
            anime.SetFloat("xVel", horizontal);
        }

        if (canVertical) {
            vertical = Input.GetAxisRaw("Vertical");
            anime.SetFloat("yVel", vertical);
        }
    }

    private void FixedUpdate() {
        //excessively slows controller movement :/
        if (horizontal != 0 && vertical != 0) { // Check for diagonal movement
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= MOVE_LIMITER;
            vertical *= MOVE_LIMITER;
        }

        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    private void ResetLoc() {
        transform.position = START_POS;
    }
}
