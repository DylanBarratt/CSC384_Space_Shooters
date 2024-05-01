using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class Fps : MonoBehaviour {
	private float count;
	
	private string filePath;
	private bool fpsEnabled = false;
	
	private IEnumerator Start() {
		ShowFpsUpdate();
		
		GUI.depth = 2;
		while (fpsEnabled) {
			count = 1f / Time.unscaledDeltaTime;
			yield return new WaitForSeconds(0.1f);
		}
	}
    
	private void OnGUI() {
		if (fpsEnabled) {
			Rect location = new Rect(5, 5, 85, 25);
			string text = $"FPS: {Mathf.Round(count)}";
			Texture black = Texture2D.linearGrayTexture;
			GUI.DrawTexture(location, black, ScaleMode.StretchToFill);
			GUI.color = Color.black;
			GUI.skin.label.fontSize = 18;
			GUI.Label(location, text);
		}
	}

	private void ShowFpsUpdate() {
		fpsEnabled = SaveSystem.LoadSettings().showFps;
	}

}
