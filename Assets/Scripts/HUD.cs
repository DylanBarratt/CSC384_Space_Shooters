using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour {
	private VisualElement root;
	private Label monies;

	public void UpdateMonies(int val) {
		//TODO: could put something funny here for when money over 999
		monies.text = "$" + val;
	}
	private void Start() {
		root = gameObject.GetComponent<UIDocument>().rootVisualElement;
		monies = root.Q<Label>("MoniesCounter");
	}
}
