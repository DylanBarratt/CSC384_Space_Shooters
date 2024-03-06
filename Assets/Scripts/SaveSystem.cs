using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
	private const string PLAYER_PATH = "player.data";

	public static string GetPlayerSavePath() {
		return Path.Combine(Application.persistentDataPath, PLAYER_PATH);
	}
	
	public static void SavePlayer(int h, float s, float rof) {
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
}
