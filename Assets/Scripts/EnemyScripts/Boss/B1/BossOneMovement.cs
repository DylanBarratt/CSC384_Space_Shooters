using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneMovement : MonoBehaviour {
	[SerializeField] private GameObject[] minis;
	private float bossSpeed = 1.5f;


	private void YReachedInit() {
		SendMessage("UpdateHealthBar", new float[] {1, 1}); //just init as full
		SendMessage("SetBossSpeed", bossSpeed); //boss one initial speed

		foreach (GameObject mini in minis) {
			mini.SendMessage("SetROF", 1f);
			mini.SendMessage("EnemyInit");
		}
	}
}
