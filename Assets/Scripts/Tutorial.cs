using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {
	[SerializeField] private GameObject tutUi, player, e1, e2;

	private int deadCount;
	
	private bool sideToSideStage, upDownStage, shootStage;

	private const float NEXT_STAGE_DELAY = 3f;
	
	private void Start() {
		deadCount = 0;
		
		sideToSideStage = false;
		upDownStage = false;
		shootStage = false;

		SideToSide();
	}

	private void Update() {
		if (Input.GetAxisRaw("Horizontal") != 0 && sideToSideStage) {
			sideToSideStage = false;
			Invoke(nameof(UpDown), NEXT_STAGE_DELAY);
		}
		
		if (Input.GetAxisRaw("Vertical") != 0 && upDownStage) {			
			upDownStage = false;
			Invoke(nameof(Shoot), NEXT_STAGE_DELAY);
		}
		
		if (Input.GetButtonUp("Fire1") && shootStage) {
			shootStage = false;
			Invoke(nameof(StaticEnemy), NEXT_STAGE_DELAY);
		}
	}

	private void SideToSide() {
		sideToSideStage = true;
		player.SendMessage("ToggleHorizontal", true);
		
		player.SendMessage("ToggleVertical", false);
		player.SendMessage("SetCanShoot", false);
		
		tutUi.SendMessage("DispSideToSide", true);
	}

	private void UpDown() {
		upDownStage = true;
		
		player.SendMessage("ToggleVertical", true);
		
		tutUi.SendMessage("DispSideToSide", false);
		tutUi.SendMessage("DispUpDown", true);
	}

	private void Shoot() {
		shootStage = true;

		player.SendMessage("SetCanShoot", true);
		tutUi.SendMessage("DispUpDown", false);
		tutUi.SendMessage("DispSpace", true);
	}

	private void StaticEnemy() {
		tutUi.SendMessage("DispSpace", false);
		GameObject eObj = Instantiate(e1);
		eObj.SetActive(true);
		eObj.gameObject.SendMessage("Move", 2f);
	}

	private void SpawnNextEnemy() {
		if (deadCount == 0) {
			StaticEnemy();
		} else if (deadCount == 1) {
			RealEnemy();
		} else if (deadCount == 2) {
			SaveSystem.SetTutorialComplete(true);
			Debug.Log(SaveSystem.IsTutorialComplete());
			SceneManager.LoadScene("Main");
		}
	}

	private void EnemyDead() {
		deadCount++;
		SpawnNextEnemy();
	}

	private void RealEnemy() {
		GameObject eObj = Instantiate(e2);
		eObj.SetActive(true);
		eObj.SendMessage("EnemyInit", new float[] {5, 2, 0});
	}
} 
