using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFourShooting : MonoBehaviour {
	[SerializeField] private Transform gunLoc;
	[SerializeField] private GameObject bulletPrefab;
	

	private void Shoot() {
		Vector2 gunLocPosition = gunLoc.position;
		GameObject bullet = Instantiate(bulletPrefab, gunLocPosition, Quaternion.identity);
		bullet.SendMessage("SetScale", 3);
	}
}
