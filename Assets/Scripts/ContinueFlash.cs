using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ContinueFlash : MonoBehaviour {
	private VisualElement root;
	private Label continueText;

	[SerializeField] private float flashInterval;

	private void Start() {
		root = gameObject.GetComponent<UIDocument>().rootVisualElement;
		continueText = root.Q<Label>("Continue");
		Invoke(nameof(Flash), flashInterval);
	}

	private void Update() {
		if (Input.anyKeyDown) {
			SceneManager.LoadScene("Menu");
		}
	}

	private void Flash() {
		continueText.visible = !continueText.visible;
		Invoke(nameof(Flash), flashInterval);
	}
}
