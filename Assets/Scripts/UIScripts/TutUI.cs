using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutUI : MonoBehaviour {
	private VisualElement root;
	private VisualElement sideToSideImg, upDownImg;
	private Label sideToSideLbl, upDownLbl1, upDownLbl2;

	private void Start() {
		root = gameObject.GetComponent<UIDocument>().rootVisualElement;

		sideToSideImg = root.Q<VisualElement>("SideToSideImg");
		upDownImg = root.Q<VisualElement>("UpDownImg");
		
		sideToSideLbl = root.Q<Label>("SideToSideLbl");
		upDownLbl1 = root.Q<Label>("UpDown1");
		upDownLbl2 = root.Q<Label>("UpDown2");
		
		HideAll();
	}
	

	private void HideAll() {
		sideToSideImg.visible = false;
		upDownImg.visible = false;
		
		sideToSideLbl.visible = false;
		upDownLbl1.visible = false;
		upDownLbl2.visible = false;
	}

	private void ShowSideToSide() {
		sideToSideImg.visible = true;
		sideToSideLbl.visible = true;
	}

	private void ShowUpDown() {
		upDownImg.visible = true;
		upDownLbl1.visible = true;
		upDownLbl2.visible = true;
	}
}
