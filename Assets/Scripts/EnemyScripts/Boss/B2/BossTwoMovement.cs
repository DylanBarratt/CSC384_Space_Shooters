using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoMovement : MonoBehaviour {
	private float shootDelay = 1f;
	private float bossSpeed = 1f;
	
	private void YReachedInit() {
		SendMessage("SetBossSpeed", bossSpeed); //boss two initial speed

		Invoke(nameof(ShootLoop), shootDelay);
	}
	
	private void ShootLoop() {
		CancelInvoke();
		SendMessage("Shoot");
		Invoke(nameof(ShootLoop), shootDelay);
	}
}
