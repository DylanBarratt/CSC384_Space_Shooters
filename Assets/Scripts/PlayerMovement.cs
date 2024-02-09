using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script with help from: https://stuartspixelgames.com/2018/06/24/simple-2d-top-down-movement-unity-c/
public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rb;
    
    private float horizontal;
    private float vertical;

    private const float SPEED = 7.0f;
    private const float MOVE_LIMITER = 0.7f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
    }

    private void FixedUpdate() {
        //excessively slows controller movement :/
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= MOVE_LIMITER;
            vertical *= MOVE_LIMITER;
        }

        rb.velocity = new Vector2(horizontal * SPEED, vertical * SPEED);
    }
}
