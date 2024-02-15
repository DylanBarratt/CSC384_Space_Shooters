using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {
	private VisualElement root;
	private Label continueText;

	private float flashInterval = 0.7f;

	private void Start() {
		root = gameObject.GetComponent<UIDocument>().rootVisualElement;
		continueText = root.Q<Label>("Continue");
		Invoke(nameof(ContinueFlash), flashInterval);
	}

	private void Update() {
		if (Input.anyKeyDown) {
			SceneManager.LoadScene("Main");
		}
	}

	private void ContinueFlash() {
		continueText.visible = !continueText.visible;
		Invoke(nameof(ContinueFlash), flashInterval);
	}
}
