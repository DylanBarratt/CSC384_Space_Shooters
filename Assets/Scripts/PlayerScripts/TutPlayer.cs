using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutPlayer : MonoBehaviour {
	[SerializeField] private GameObject tutObj;
	private bool killed = false;

	private void DamagePlayer() {
		if (!killed) {			
			killed = true;
			Destroy(GameObject.FindWithTag("Enemy"));
			KillPlayer();
		}
	}
	
	private void KillPlayer() { 
		tutObj.SendMessage("SpawnNextEnemy");
		gameObject.SendMessage("ResetLoc");
		Invoke(nameof(RemoveInvincibility), 1f);
	}

	private void RemoveInvincibility() {
		killed = false;
	}
}
