using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//basically stole the player movement class xD
public class EnemyFourMovement : MonoBehaviour { 
	private Rigidbody2D rb;
	private Animator anime;
    
	private float horizontal;
	private float vertical;
	private float speed;
	
	private const float MOVE_LIMITER = 0.7f;
	
	private bool yReach;
	
	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		anime = GetComponent<Animator>();
	}

	private void YReached(float s) {
		speed = s;
		yReach = true;
	}

	private void Update() {
		if (!yReach) return;
		

		horizontal = -Input.GetAxisRaw("Horizontal");
		vertical = -Input.GetAxisRaw("Vertical");
			
		anime.SetFloat("xVel", horizontal);
		anime.SetFloat("yVel", vertical);
			
		//Means e4 only shoots on press and release. Allows player a chance to ded them - Dylan
		if (Input.GetButtonDown("Fire1")){
			SendShoot();
		}

		if (Input.GetButtonUp("Fire1")){
			SendShoot();
		}
	}

	private void SendShoot() {
		gameObject.SendMessage("Shoot");
	}
	
	private void FixedUpdate() {
		if (!yReach) return;

		if (rb.position.y >= 5.5) {
			vertical = -Math.Abs(vertical); //never let movement go past 5.5
		} 
		
		
		if (horizontal != 0 && vertical != 0) {
			// limit movement speed diagonally, so you move at 70% speed
			horizontal *= MOVE_LIMITER;
			vertical *= MOVE_LIMITER;
		}

		rb.velocity = new Vector2(horizontal * speed, vertical * speed);
	}
}

