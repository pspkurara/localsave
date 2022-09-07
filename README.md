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

Later entry.

* ExternalSavePrefs
* ISavePrefs
* ILocalSaveSystem and LocalSaveSettings

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
