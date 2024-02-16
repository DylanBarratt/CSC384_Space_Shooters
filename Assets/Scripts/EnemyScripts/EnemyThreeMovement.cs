using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyThreeMovement : MonoBehaviour {
	private Transform playerPos;
	
	private float speed;
	
	//TODO: Delete!
	private void Start() {
		Move(4f);
	}

	private void Move(float s) {
		speed = s;
		playerPos = GameObject.FindWithTag("Player").transform;
	}

	private void Update() {
		float step = speed * Time.deltaTime;

		// move sprite towards the target location
		transform.position = Vector2.MoveTowards(transform.position, playerPos.position, step);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Boundary")) {
			transform.position = new Vector3(Random.Range(-3, 3), 10);
		}
	}
}
