using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour {
	[SerializeField] private GameObject[] bossess;
	
	private void SpawnBoss(int bossID) {
		switch (bossID) {
			case 1: 
				b1();
				break;
			case 2: 
				b2();
				break;
			case 3: 
				b3();
				break;
			case 4: 
				b4();
				break;
		}
	}

	private void b1() {
		Instantiate(bossess[0], new Vector3(0, 18, 0), bossess[0].transform.rotation);
	}

	private void b2() {
		
	}

	private void b3() {
		
	}

	private void b4() {
		
	}


}
