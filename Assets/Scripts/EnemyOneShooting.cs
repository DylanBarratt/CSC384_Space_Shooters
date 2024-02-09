using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneShooting : MonoBehaviour {
	[SerializeField] private Transform gunLoc;
	[SerializeField] private GameObject bulletPrefab;
	
	private float rateOfFire;
	
	public void ShootStart(float rof) {
		rateOfFire = rof;
		Shoot();
	}

	private void Shoot() {
		Vector2 gunLocPosition = gunLoc.position;
		float bulletOffset = 0.25f;
		Vector2 lSpawn = new Vector2(gunLocPosition.x - bulletOffset, gunLocPosition.y);
		Vector2 rSpawn = new Vector2(gunLocPosition.x + bulletOffset, gunLocPosition.y);
		
		Instantiate(bulletPrefab, lSpawn, Quaternion.identity);
		Instantiate(bulletPrefab, rSpawn, Quaternion.identity);
		Invoke(nameof(Shoot), rateOfFire);
	}
}
