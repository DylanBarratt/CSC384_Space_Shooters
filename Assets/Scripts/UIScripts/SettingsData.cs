using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData {
	public bool showFps = false, ezMode = false;

	public SettingsData(bool sFps, bool ez) {
		showFps = sFps;
		ezMode = ez;
	}
}
