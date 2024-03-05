using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneMovement : MonoBehaviour {
	[SerializeField] private GameObject[] minis;
	
	private void YReached() {
		foreach (GameObject mini in minis) {
			mini.SendMessage("Shoot");
		}
	}
}
