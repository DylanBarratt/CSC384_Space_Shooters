using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {
	private VisualElement root;
	private Button backBtn, delBtn;
	private Toggle fpsToggle, ezToggle;
	private string filePath; 
	
	private void Start() {
		filePath = Application.persistentDataPath + "/settings.txt";
		
		root = gameObject.GetComponent<UIDocument>().rootVisualElement;
		backBtn = root.Q<Button>("Back");
		delBtn = root.Q<Button>("DeleteSave");
		fpsToggle = root.Q<Toggle>("Fps");
		ezToggle = root.Q<Toggle>("Ez");
		
		backBtn.clicked += () => SceneManager.LoadScene("Menu");
		delBtn.clicked += () => DeleteSave();
			
		//get rid of stupid blue text...
		fpsToggle.focusable = false;
		ezToggle.focusable = false;
		
		LoadToggleValues();

		fpsToggle.RegisterValueChangedCallback(ToggleFps);
		ezToggle.RegisterValueChangedCallback(ToggleEz);
	}

	private void LoadToggleValues() {
		if (File.Exists(filePath)) {
			string[] savedValues = File.ReadAllLines(filePath);
			if (savedValues.Length >= 2) {
				fpsToggle.value = bool.Parse(savedValues[0]);
				ezToggle.value = bool.Parse(savedValues[1]);
			}
		}
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
		string[] values = { fpsToggle.value.ToString(), ezToggle.value.ToString() };
		File.WriteAllLines(filePath, values);
		SendMessage("ShowFpsUpdate");
	}

	private void DeleteSave() {
		SaveSystem.DeletePlayer();
		Debug.Log("Player deleted!");
	}
}
