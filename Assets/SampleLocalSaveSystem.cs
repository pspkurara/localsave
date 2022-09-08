using UnityEngine;
using System.Collections.Generic;
using Pspkurara.LocalSave;
using System.IO;

public class SampleLocalSaveSystem : MonoBehaviour
{
	private static IntSavePrefs SampleLocalSave = new IntSavePrefs("SAVE_SAMPLE_LOCAL_SAVE_KEY");
	private static IntSavePrefs SampleLocalSave2;

	private void Start()
	{
		// your own save system.
		// output for Assets/.sample_save.txt.
		var fileLocalSaveSystem = new FileLocalSaveSystem();

		// replace all default save systems.
		LocalSaveSettings.SaveSystem = fileLocalSaveSystem;

		// or it is also possible to specify with an argument individually.
		SampleLocalSave2 = new IntSavePrefs("SAVE_SAMPLE_LOCAL_SAVE_2_KEY", fileLocalSaveSystem);

		Debug.Log(SampleLocalSave.GetValue());      // output: 0 (default value)

		Debug.Log(SampleLocalSave.GetValue(1));     // output: 1

		SampleLocalSave.SetValue(2);                // write value.
		Debug.Log(SampleLocalSave.GetValue());      // output : 2

		SampleLocalSave2.SetValue(3);                // write value.
		Debug.Log(SampleLocalSave2.GetValue());      // output : 3
	}
}

public class FileLocalSaveSystem : ILocalSaveSystem
{

	// raw data array.
	[System.Serializable]
	public class JsonArray
	{
		public List<JsonData> Array = new List<JsonData>();
	}

	// raw data element.
	[System.Serializable]
	public class JsonData
	{
		public string Key;
		public string Value;
	}

	// raw data json file path.
	private const string SaveFilePath = "Assets/.sample_save.txt";

	private bool ReadFile(string key, out string result)
	{
		// get save from file.
		result = null;
		try
		{
			// load file.
			if (!File.Exists(SaveFilePath)) return false;
			result = File.ReadAllText(SaveFilePath);
			// to json convert.
			JsonArray jsonResult = JsonUtility.FromJson<JsonArray>(result);
			// find data from key.
			int jsonArrayIndex = jsonResult.Array.FindIndex(j => j.Key == key);
			// if not exists, failed.
			if (jsonArrayIndex == -1) return false;
			// set data for result.
			result = jsonResult.Array[jsonArrayIndex].Value;
			// success.
			return true;
		}
		catch (System.Exception)
		{
			// failed load.
			return false;
		}
	}

	// set save to file.
	private void WriteFile(string key, string value)
	{
		try
		{
			// load file.
			if (!File.Exists(SaveFilePath)) return;
			string rawJson = File.ReadAllText(SaveFilePath);
			// to json convert.
			JsonArray jsonResult = JsonUtility.FromJson<JsonArray>(rawJson);
			// find data from key.
			int jsonArrayIndex = jsonResult.Array.FindIndex(j => j.Key == key);
			// key exists or not exists.
			if (jsonArrayIndex != -1)
			{
				// update value.
				jsonResult.Array[jsonArrayIndex].Value = value;
			}
			else
			{
				// add new value.
				jsonResult.Array.Add(new JsonData() { Key = key, Value = value });
			}
			// write on file.
			string updatedJson = JsonUtility.ToJson(jsonResult);
			File.WriteAllText(SaveFilePath, updatedJson);
		}
		// failed.
		catch (System.Exception) { }
	}

	public bool HasKey(string key)
	{
		// get save from file.
		try
		{
			// load file.
			if (!File.Exists(SaveFilePath)) return false;
			string rawJson = File.ReadAllText(SaveFilePath);
			// to json convert.
			JsonArray jsonResult = JsonUtility.FromJson<JsonArray>(rawJson);
			// check exists key.
			return jsonResult.Array.Exists(j => j.Key == key);
		}
		catch (System.Exception)
		{
			// failed load.
			return false;
		}
	}

	public float GetFloat(string key, float defaultValue)
	{
		if (!ReadFile(key, out string result)) return defaultValue;
		return float.Parse(result);
	}

	public int GetInt(string key, int defaultValue)
	{
		if (!ReadFile(key, out string result)) return defaultValue;
		return int.Parse(result);
	}

	public string GetString(string key, string defaultValue)
	{
		if (!ReadFile(key, out string result)) return defaultValue;
		return result;
	}

	public void SetFloat(string key, float value)
	{
		WriteFile(key, value.ToString());
	}

	public void SetInt(string key, int value)
	{
		WriteFile(key, value.ToString());
	}

	public void SetString(string key, string value)
	{
		WriteFile(key, value);
	}
}
