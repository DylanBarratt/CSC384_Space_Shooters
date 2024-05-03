using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneMovement : MonoBehaviour {
	[SerializeField] private GameObject[] minis;
	private float bossSpeed = 1.5f;


	private void YReachedInit() {
		SendMessage("UpdateHealthBar", new float[] {100, 100}); //just init as full
		SendMessage("SetBossSpeed", bossSpeed); //boss one initial speed

		Debug.Log("Enable yo");
		foreach (GameObject mini in minis) {
			Behaviour[] scripts = mini.GetComponents<Behaviour>();

			// Enable all script components
			foreach (Behaviour script in scripts) {
				script.enabled = true;
			}

			mini.SendMessage("SetROF", 1f);
			mini.SendMessage("EnemyInit", true);
		}
	}
}
