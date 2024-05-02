using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;

public static class SaveSystem {
	private const string PLAYER_PATH = "player.data";
	private const string TUTORIAL_PATH = "tut.data";
	private const string SETTINGS_PATH = "settings.data";
	private const string WINS_PATH = "wins.data";

	public static string GetPlayerSavePath() {
		return Path.Combine(Application.persistentDataPath, PLAYER_PATH);
	}
	
	public static void SavePlayer(int h, float s, float rof) {
		Debug.Log("Saving health: " + h);
		FileStream stream = new FileStream(GetPlayerSavePath(), FileMode.Create);

		PlayerData data = new PlayerData(h, s, rof);
		
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static PlayerData LoadPlayer() {
		string path = GetPlayerSavePath();

		if (!File.Exists(path)) {
			Debug.LogError("Player file does not exist: " + path);
			return null;
		}
		
		FileStream stream = new FileStream(path, FileMode.Open);
		
		BinaryFormatter formatter = new BinaryFormatter();
		PlayerData data = formatter.Deserialize(stream) as PlayerData;
		
		stream.Close();
		
		return data;
	}

	public static void DeletePlayer() {
		string path = GetPlayerSavePath();

		if (!File.Exists(path)) {
			Debug.LogError("Player file does not exist: " + path);
			return;
		}
		
		File.Delete(path);
	}
	
	public static string GetTutorialPath() {
		return Path.Combine(Application.persistentDataPath, TUTORIAL_PATH);
	}
	
	public static void SetTutorialComplete(bool val) {
		FileStream stream = new FileStream(GetTutorialPath(), FileMode.Create);
		
		BinaryFormatter formatter = new BinaryFormatter();
		SaveData data = new SaveData(val);
		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static bool IsTutorialComplete() {
		string path = GetTutorialPath();

		if (!File.Exists(path)) {
			return false;
		}
		
		FileStream stream = new FileStream(path, FileMode.Open);
		
		BinaryFormatter formatter = new BinaryFormatter();
		SaveData data = formatter.Deserialize(stream) as SaveData;
		stream.Close();

		if (data == null) {
			return false;
		}
		
		return data.tutorialComplete;
	}
	
	public static string GetSettingsPath() {
		return Path.Combine(Application.persistentDataPath, SETTINGS_PATH);
	}

	public static void SaveSettings(bool showFps, bool ezMode) {
		FileStream stream = new FileStream(GetSettingsPath(), FileMode.Create);

		SettingsData data = new SettingsData(showFps, ezMode);
		
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(stream, data);
		stream.Close();
	}
	
	public static SettingsData LoadSettings() {
		string path = GetSettingsPath();

		if (!File.Exists(path)) {
			Debug.LogError("Settings file does not exist: " + path);
			return new SettingsData(false, false);
		}
		
		FileStream stream = new FileStream(path, FileMode.Open);
		
		BinaryFormatter formatter = new BinaryFormatter();
		SettingsData data = formatter.Deserialize(stream) as SettingsData;
		stream.Close();

		if (data == null) {
			return new SettingsData(false, false);
		}
		
		return data;
	}
	
	public static string GetWinsSavePath() {
		return Path.Combine(Application.persistentDataPath, WINS_PATH);
	}

	public static void ZeroWins() {
		FileStream stream = new FileStream(GetWinsSavePath(), FileMode.Create);

		WinsData data = new WinsData(0);
		
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static void SaveWin() {
		string path = GetWinsSavePath();
		
		if (!File.Exists(path)) {
			Debug.LogError("Wins file does not exist: " + path);
			ZeroWins();
			return;
		}
		
		int numWins = GetNumWins();
		
		FileStream stream = new FileStream(path, FileMode.Create);
		WinsData data = new WinsData(numWins + 1);
		
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static int GetNumWins() {
		string path = GetWinsSavePath();

		if (!File.Exists(path)) {
			Debug.LogError("Wins file does not exist: " + path);
			ZeroWins();
			return 0;
		}
		
		FileStream stream = new FileStream(path, FileMode.Open);
		
		BinaryFormatter formatter = new BinaryFormatter();
		WinsData data = formatter.Deserialize(stream) as WinsData;
		stream.Close();

		if (data == null) {
			return 0;
		}
		
		return data.numWins;
	}
}
