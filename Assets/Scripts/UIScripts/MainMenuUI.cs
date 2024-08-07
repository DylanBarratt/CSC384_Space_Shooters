using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour {
	private VisualElement root;
	private Button startBtn, optionsBtn, exitBtn;
	private Label winsLbl;
	
	private void Start() {
		root = gameObject.GetComponent<UIDocument>().rootVisualElement;
		startBtn = root.Q<Button>("Play");
		optionsBtn = root.Q<Button>("Options");
		exitBtn = root.Q<Button>("Exit");

		winsLbl = root.Q<Label>("WinsLbl");
		winsLbl.text = "Wins: " + SaveSystem.GetNumWins();
		
		startBtn.clicked += () => StartGame();
		optionsBtn.clicked += () => ChangeScene("Options");
		exitBtn.clicked += () => Exit();
	}

	private void StartGame() {
Debug.Log(SaveSystem.IsTutorialComplete());
		if (SaveSystem.IsTutorialComplete()) {
			ChangeScene("Main");
		} else {
			ChangeScene("Tut");
		}
	}

	private void ChangeScene(string index) {
		SceneManager.LoadScene(index);
	}

	private void Exit() {
		Debug.Log("Goodbye :)");
		Application.Quit();
	}
}
