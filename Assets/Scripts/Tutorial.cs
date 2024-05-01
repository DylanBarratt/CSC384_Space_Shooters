using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
	[SerializeField] private GameObject tutUi;
	
	private void Start() {
		Invoke(nameof(SideToSide), 1f);
	}

	private void SideToSide() {
		tutUi.SendMessage("ShowSideToSide");
	}
}
