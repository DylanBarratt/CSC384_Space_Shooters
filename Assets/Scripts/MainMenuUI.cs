using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour {
	private VisualElement root;
	private Button startBtn, optionsBtn, statsBtn, exitBtn;
	
	private void Start() {
		root = gameObject.GetComponent<UIDocument>().rootVisualElement;
		startBtn = root.Q<Button>("Play");
		optionsBtn = root.Q<Button>("Options");
		statsBtn = root.Q<Button>("Stats");
		exitBtn = root.Q<Button>("Exit");
		
		startBtn.clicked += () => ChangeScene("Main");
		optionsBtn.clicked += () => ChangeScene("Options");
		statsBtn.clicked += () => ChangeScene("Stats");
		exitBtn.clicked += () => Exit();
	}

	private void ChangeScene(string index) {
		SceneManager.LoadScene(index);
	}

	private void Exit() {
		Application.Quit();
	}
}
