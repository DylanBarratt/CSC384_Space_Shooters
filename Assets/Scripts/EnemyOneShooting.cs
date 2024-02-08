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
		Instantiate(bulletPrefab, gunLoc.position, Quaternion.identity);
		Invoke(nameof(Shoot), rateOfFire);
	}
}
