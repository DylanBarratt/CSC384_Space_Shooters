using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ETut1 : MonoBehaviour {
	[SerializeField] private GameObject tutObj;
	
	private bool initiated = false;
	private float health = 7, speed;
	
	private Animator anime;
	
	public void EnemyInit(float[] vals) {
		health = vals[0];
		speed = vals[1];

		
		if (speed != 0) {
			gameObject.SendMessage("Move", speed);
		}
		initiated = true;
	}

	private void Start() {
		anime = GetComponent<Animator>();
	}

	private void YReached() {
		initiated = true;
	}
	
	private void Damage(int amount) {
		if (!initiated) return;

		health -= amount;
		
		if (health <= 0) {
			tutObj.SendMessage("EnemyDead");
			KillEnemy();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		switch (collision.gameObject.tag) {
			case "Player":
				KillEnemy();
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (!initiated) return;
		
		switch (collision.gameObject.tag) {
			case "PlayerBullet":
				Damage(1);
				break;
		}
	}
	
	private void KillEnemy() {
		Destroy(GetComponent<BoxCollider2D>());
		Destroy(GetComponent<Rigidbody2D>());
		
		Component[] allComponents = GetComponents(typeof(MonoBehaviour));
		foreach (Component gameObjectComponent in allComponents) Destroy((MonoBehaviour)gameObjectComponent);
		
		anime.SetBool("ded", true);
		
		Destroy(gameObject, 1f);
	}
}
