# LocalSave

Supports Unity's simple local save utility.

[![](https://img.shields.io/npm/v/com.pspkurara.localsave?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.pspkurara.localsave/)
[![](https://img.shields.io/github/v/release/pspkurara/localsave)](https://github.com/pspkurara/localsave/releases/)
[![](https://img.shields.io/github/watchers/pspkurara/localsave?style=social)](https://github.com/pspkurara/external-selecion-state/subscription)

## Usage

### Simple use

Declare a static variable in the XXSavePrefs class and specify the PlayerPrefs key as an argument.
bool, int, bool, float, string, enum, class, struct are supported.
Be careful not to duplicate key values within the project you are using.
```
using UnityEngine;
using Pspkurara.LocalSave;

public static class SampleSaves
{
	public static readonly IntSavePrefs SampleIntegerSaveData = new IntSavePrefs("SAMPLE_INTEGER_SAVE_KEY");
	public static readonly BoolSavePrefs SampleBooleanSaveData = new BoolSavePrefs("SAMPLE_BOOLEAN_SAVE_KEY");
	public static readonly StringSavePrefs SampleStringSaveData = new StringSavePrefs("SAMPLE_STRING_SAVE_KEY");
	public static readonly FloatSavePrefs SampleFloatSaveData = new FloatSavePrefs("SAMPLE_FLOAT_SAVE_KEY");
	public static readonly EnumSavePrefs<EnumSample> SampleEnumSaveData = new EnumSavePrefs<EnumSample>("SAMPLE_ENUM_SAVE_KEY");
}

public enum EnumSample
{
	State1,
	State2,
}
```

Classes and structs with public and SerializeField attributes are stored internally in Json.
Specify the type you want to use in the generic.
Anything that can be serialized in Unity is supported.
```
using UnityEngine;
using Pspkurara.LocalSave;

public static class SampleSaves
{
	public static readonly StructSavePrefs<Vector3> SampleVector3SaveData = new StructSavePrefs<Vector3>("SAMPLE_VECTOR3_SAVE_KEY");
	public static readonly ClassSavePrefs<DataClass> SampleClassSaveData = new ClassSavePrefs<DataClass>("SAMPLE_CLASS_SAVE_KEY");
}

[System.Serializable]
public class DataClass
{
	public int SampleValue;
	public int[] SampleArray = System.Array.Empty<int>();
	
	public override string ToString()
	{
		return "Value: " + SampleValue.ToString() + ", Length: " + SampleArray.Length.ToString();
	}
}
```

Reads or write datas.
```
using UnityEngine;

public class SampleSystemLogic : MonoBehaviour
{
	private void Start()
	{
		// primitive value
		Debug.Log(SampleSaves.SampleIntegerSaveData.GetValue());	// output: 0 (default value)
		
		Debug.Log(SampleSaves.SampleIntegerSaveData.GetValue(1));	// output: 1 (If a value is specified for the argument, it will be the default value)
		
		SampleSaves.SampleIntegerSaveData.SetValue(2);				// write: 2
		Debug.Log(SampleSaves.SampleIntegerSaveData.GetValue());	// output: 2 (Once written, the value is returned)
		
		// -----------
		
		// class value (ToString overrided)
		Debug.Log(SampleSaves.SampleClassSaveData.GetValue());		// output: null (default value)
		
		Debug.Log(SampleSaves.SampleClassSaveData.GetValue(new DataClass()));	// output: Value: 0. Length: 0 (If a value is specified for the argument, it will be the default value)
		
		Debug.Log(SampleSaves.SampleClassSaveData.GetValue(()=>new DataClass()));	// output: Value: 0. Length: 0 (Generation methods can be used as arguments to avoid wasting heap space when data is available)
		
		DataClass tempData = SampleSaves.SampleClassSaveData.GetValue(new DataClass());
		tempData.SampleValue = 10;
		SampleSaves.SampleClassSaveData.SetValue(tempData);			// write: simple value is default to 10
		Debug.Log(SampleSaves.SampleClassSaveData.GetValue());		// output: output: Value: 10. Length: 0 (Once written, the value is returned)
	}
}
```

### Advanced use

#### Save and load from outside the library

Use ExternalSavePrefs.
Specify methods and inject behaviors when declaring variables.
```
using UnityEngine;
using System.Collections.Generic;
using Pspkurara.LocalSave;

public class SampleExternalSavePrefs : MonoBehaviour
{

	private ExternalSavePrefs<int> ExtSave;

	private Dictionary<string, int> m_RawSaveData = new Dictionary<string, int>();

	private void Start()
    {
		// declared here to set from an instance member.
		ExtSave = new ExternalSavePrefs<int>("EXT_SAVE_KEY", GetExtValue, SetExtValue);

		Debug.Log(ExtSave.GetValue());	// output: 0 (default value)

		Debug.Log(ExtSave.GetValue(1)); // output: 1

		ExtSave.SetValue(2);			// write value.
		Debug.Log(ExtSave.GetValue());	// output : 2
	}

	private int GetExtValue(string key, int defaultValue)
	{
		// find and return saved or default value.
		if (!m_RawSaveData.ContainsKey(key)) return defaultValue;
		return m_RawSaveData[key];
	}

	private void SetExtValue(string key, int value)
	{
		// add or update value.
		if (m_RawSaveData.ContainsKey(key)) m_RawSaveData.Add(key, value);
		else m_RawSaveData[key] = value;
	}

}
```

#### Want to make the existing storage system compatible with the new type

Use SavePrefsBase.
All SavePrefs are derived from this class.
By inheriting, you can create your own completely new type-aware SavePrefs.
```
using UnityEngine;
using Pspkurara.LocalSave;

public class SampleSavePrefsBase : MonoBehaviour
{

	private static LongSavePrefs LongSave = new LongSavePrefs("LONG_SAVE_KEY");

	// Start is called before the first frame update
	void Start()
	{
		Debug.Log(LongSave.GetValue());		// output: 0 (default value)

		Debug.Log(LongSave.GetValue(1));	// output: 1

		LongSave.SetValue(long.MaxValue);	// write value.
		Debug.Log(LongSave.GetValue());     // output : 9223372036854775807 (long.MaxValue)
	}
}

// proprietary system that supports long save.
public class LongSavePrefs : SavePrefsBase<long>
{
	public LongSavePrefs(string key) : base(key) { }
	public LongSavePrefs(string key, ILocalSaveSystem saveSystem) : base(key, saveSystem) { }

	public override long GetValue(long defaultValue = 0)
	{
		// find key and if not exist, return default.
		if (!SaveSystem.HasKey(Key)) return defaultValue;
		// long to string.
		return long.Parse(SaveSystem.GetString(Key, null));
	}

	public override void SetValue(long value)
	{
		// string to long.
		SaveSystem.SetString(Key, value.ToString());
	}
}
```

#### Want to handle SavePrefs consistently

Use ISavePrefs.
SavePrefsBase is Generic, but inherits from ISavePrefs and all SavePrefs can be batched through it.
```
using UnityEngine;
using Pspkurara.LocalSave;

public class SampleISavePrefs : MonoBehaviour
{
	private static IntSavePrefs Save1 = new IntSavePrefs("SAVE_1_KEY");
	private static BoolSavePrefs Save2 = new BoolSavePrefs("SAVE_2_KEY");
	private static StringSavePrefs Save3 = new StringSavePrefs("SAVE_3_KEY");

    private void Start()
    {
		// to combined data.
		var savePrefsArray = new ISavePrefs[] { Save1, Save2, Save3 };

		foreach (var save in savePrefsArray)
		{
			Debug.Log(save.Key); // output : SAVE_?_KEY (various save keys)
		}
    }
}
```

#### Want to save to a location other than PlayerPrefs

Use ILocalSaveSystem and LocalSaveSettings.
ILocalSaveSystem allows you to decide where to save your own raw data and how it behaves.
LocalSaveSettings contains the base SaveSystem for all SavePrefs, so you can replace them all at once.

```
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

		Debug.Log(SampleLocalSave.GetValue());		// output: 0 (default value)

		Debug.Log(SampleLocalSave.GetValue(1));		// output: 1

		SampleLocalSave.SetValue(2);				// write value.
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
```

### Full API references
* https://pspkurara.github.io/localsave/

## Installation

### Using OpenUPM
Go to Unity's project folder on the command line and call:

```
openupm add com.pspkurara.localsave
```

### Using Unity Package Manager (For Unity 2018.3 or later)
Find the manifest.json file in the Packages folder of your project and edit it to look like this:

```
{
  "dependencies": {
    "com.pspkurara.localsave": "https://github.com/pspkurara/localsave.git#upm",
    ...
  },
}
```

#### Requirement
Unity 2018.1 or later<br>
May work in Unity5, but unofficial.

## License

* [MIT](https://github.com/pspkurara/localsave/blob/master/Packages/LocalSave/LICENSE.md)

## Author

* [pspkurara](https://github.com/pspkurara) 
[![](https://img.shields.io/twitter/follow/pspkurara.svg?label=Follow&style=social)](https://twitter.com/intent/follow?screen_name=pspkurara) 

## See Also

* GitHub page : https://github.com/pspkurara/localsave
