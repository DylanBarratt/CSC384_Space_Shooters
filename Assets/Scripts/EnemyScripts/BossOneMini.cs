using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneMini : MonoBehaviour {
	[SerializeField] private GameObject main;
	
	private Animator anime;
	
	private void Start() {
		anime = GetComponent<Animator>();
	}

	private void KillEnemy() {
		main.SendMessage("MiniKilled");
		
		Destroy(GetComponent<BoxCollider2D>());
		Destroy(GetComponent<Rigidbody2D>());
		
		Component[] allComponents = GetComponents(typeof(MonoBehaviour));
		foreach (Component gameObjectComponent in allComponents) Destroy((MonoBehaviour)gameObjectComponent);
		
		anime.SetBool("ded", true);
		
		Destroy(gameObject, 1f);
	}
}
