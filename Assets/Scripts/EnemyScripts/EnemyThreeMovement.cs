using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyThreeMovement : MonoBehaviour {
	[SerializeField] private Transform respawnLocs;
	private Rigidbody2D rb;

	private Transform playerPos;
	
	private float speed;
	
	//TODO: Delete!
	private void Start() {
		Move(4f);
	}

	private void Move(float s) {
		speed = s;
		
		rb = GetComponent<Rigidbody2D>();
		playerPos = GameObject.FindWithTag("Player").transform;
	}

	private void Update() {
		float step = speed * Time.deltaTime;

		// move sprite towards the target location
		transform.position = Vector2.MoveTowards(transform.position, playerPos.position, step);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Boundary")) {
			transform.position = respawnLocs.position;
		}
	}
}
