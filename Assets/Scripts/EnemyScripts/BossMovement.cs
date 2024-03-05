using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {

	private Vector2 targetY = new Vector2(0, 10);

	private float speed = 3f;
	
	private void Update() {
		transform.Translate(Vector3.up * Time.deltaTime * speed);
		
		if (transform.position.y <= targetY.y) {
			gameObject.SendMessage("YReached", speed);
			Destroy(this);
		}
	}
}
