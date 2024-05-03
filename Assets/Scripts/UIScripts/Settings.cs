using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {
	private VisualElement root;
	private Button backBtn, tutBtn, delBtn;
	private Toggle fpsToggle, ezToggle;
	
	private void Start() {
		root = gameObject.GetComponent<UIDocument>().rootVisualElement;
		backBtn = root.Q<Button>("Back");
		tutBtn = root.Q<Button>("TutorialBtn");
		delBtn = root.Q<Button>("DeleteSave");
		fpsToggle = root.Q<Toggle>("Fps");
		ezToggle = root.Q<Toggle>("Ez");
		
		backBtn.clicked += () => SceneManager.LoadScene("Menu");
		tutBtn.clicked += () => SceneManager.LoadScene("Tut");
		delBtn.clicked += () => DeleteSave();
		
			
		//get rid of stupid blue text...
		fpsToggle.focusable = false;
		ezToggle.focusable = false;
		
		LoadToggleValues();

		fpsToggle.RegisterValueChangedCallback(ToggleFps);
		ezToggle.RegisterValueChangedCallback(ToggleEz);
	}

	private void LoadToggleValues() {
		SettingsData data = SaveSystem.LoadSettings();
		fpsToggle.value = data.showFps;
		ezToggle.value = data.ezMode;
	}

	private void ToggleFps(ChangeEvent<bool> evt) {
		if (fpsToggle != null) {
			Debug.Log("Show fps: " + fpsToggle.value);
			SaveToggleValues();
		}
	}
	
	private void ToggleEz(ChangeEvent<bool> evt) {
		if (ezToggle != null) {
			Debug.Log("ez mode: " + ezToggle.value);
			SaveToggleValues();
		}
	}
	
	private void SaveToggleValues() {
		SaveSystem.SaveSettings(fpsToggle.value, ezToggle.value);
		SendMessage("ShowFpsUpdate");
	}

	private void DeleteSave() {
		SaveSystem.DeletePlayer();
		Debug.Log("Player deleted!");
		
		SaveSystem.ZeroWins();
		Debug.Log("Save deleted!");

		SaveSystem.SetTutorialComplete(false);
		Debug.Log("Tutorial deleted!");
	}
}
