using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoShooting : MonoBehaviour {
	[SerializeField] private Transform gunLoc;
	[SerializeField] private GameObject bulletPrefab;

	private float scale;
	private Vector2 gunLocPosition;

	private void Shoot() {
		gunLocPosition = gunLoc.position; //so all bullets come from the same place

		scale = 5;
		Bullet();
		Invoke(nameof(Bullet), 0.1f);
		Invoke(nameof(Bullet), 0.25f);
	}

	private void Bullet() {
		scale *= 2;

		GameObject bullet1 = Instantiate(bulletPrefab, gunLocPosition, Quaternion.identity);
		bullet1.SendMessage("SetScale", scale);
	}
}
