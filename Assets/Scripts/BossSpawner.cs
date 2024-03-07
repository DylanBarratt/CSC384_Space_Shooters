using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossSpawner : MonoBehaviour {
	[SerializeField] private GameObject[] bosses;
	
	private void SpawnBoss(int bossID) {
		Instantiate(bosses[bossID - 1], new Vector3(0, 18, 0), bosses[bossID - 1].transform.rotation);
	}
}
