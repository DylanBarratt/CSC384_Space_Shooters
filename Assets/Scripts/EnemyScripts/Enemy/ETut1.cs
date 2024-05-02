using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ETut1 : MonoBehaviour {
	[SerializeField] private GameObject tutObj;
	[SerializeField] private EnemyType enemyStats;

	private bool initiated = false;
	
	private Animator anime;
	
	private float health;
	
	private void Start() {
		Debug.Log(enemyStats.health);
		health = enemyStats.health;
		anime = GetComponent<Animator>();
	}
	
	public void EnemyInit() {
		if (enemyStats.speed != 0) {
			gameObject.SendMessage("Move", enemyStats.speed);
		}
		initiated = true;
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
