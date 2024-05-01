using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneMovement : MonoBehaviour {
	[SerializeField] private GameObject[] minis;

	private GameObject UIGameObject;
	private GameObject Starz;

	private EnemyData vals = new EnemyData(7, 0, 0);
	private float speed = 1.5f;

	private bool yReach;
	
	private void Start() {
		UIGameObject = GameObject.Find("GameManager/HUD_UI");
		Starz = GameObject.Find("Starz/bg");
	}
	
	private void YReached() {
		foreach (GameObject mini in minis) {
			mini.SendMessage("SetROF", 1f);
			mini.SendMessage("EnemyInit", vals);
		}
		
		UIGameObject.SendMessage("DisplayBossBar", true);
		
		Starz.SendMessage("SetSpeed", 0.00001f);

		yReach = true;
	}

	private void Update() {
		if (!yReach) return;
		
		transform.Translate(Vector3.left * Time.deltaTime * speed);
	}
	
	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("SBoundary")) {
			speed = -speed;  // neat refactoring came from this cool line 
		}
	}
}
