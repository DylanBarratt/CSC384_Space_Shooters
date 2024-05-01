using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutUI : MonoBehaviour {
	private VisualElement root;
	private VisualElement sideToSideImg, upDownImg, spaceImg;
	private Label sideToSideLbl, upDownLbl1, upDownLbl2;

	private void Start() {
		root = gameObject.GetComponent<UIDocument>().rootVisualElement;

		sideToSideImg = root.Q<VisualElement>("SideToSideImg");
		upDownImg = root.Q<VisualElement>("UpDownImg");
		spaceImg = root.Q<VisualElement>("SpaceImg");
		
		sideToSideLbl = root.Q<Label>("SideToSideLbl");
		upDownLbl1 = root.Q<Label>("UpDown1");
		upDownLbl2 = root.Q<Label>("UpDown2");
		
		HideAll();
	}
	

	private void HideAll() {
		sideToSideImg.visible = false;
		sideToSideLbl.visible = false;
		
		upDownImg.visible = false;
		upDownLbl1.visible = false;
		upDownLbl2.visible = false;
		
		spaceImg.visible = false;
	}

	private void DispSideToSide(bool val) {
		sideToSideImg.visible = val;
		sideToSideLbl.visible = val;
	}

	private void DispUpDown(bool val) {
		upDownImg.visible = val;
		upDownLbl1.visible = val;
		upDownLbl2.visible = val;
	}

	private void DispSpace(bool val) {
		spaceImg.visible = val;
	}
}
