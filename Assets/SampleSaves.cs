using UnityEngine;
using Pspkurara.LocalSave;

public static class SampleSaves
{
	public static readonly IntSavePrefs SampleIntegerSaveData = new IntSavePrefs("SAMPLE_INTEGER_SAVE_KEY");
	public static readonly BoolSavePrefs SampleBooleanSaveData = new BoolSavePrefs("SAMPLE_BOOLEAN_SAVE_KEY");
	public static readonly StringSavePrefs SampleStringSaveData = new StringSavePrefs("SAMPLE_STRING_SAVE_KEY");
	public static readonly FloatSavePrefs SampleFloatSaveData = new FloatSavePrefs("SAMPLE_FLOAT_SAVE_KEY");
	public static readonly EnumSavePrefs<EnumSample> SampleEnumSaveData = new EnumSavePrefs<EnumSample>("SAMPLE_ENUM_SAVE_KEY");
	public static readonly StructSavePrefs<Vector3> SampleVector3SaveData = new StructSavePrefs<Vector3>("SAMPLE_VECTOR3_SAVE_KEY");
	public static readonly ClassSavePrefs<DataClass> SampleClassSaveData = new ClassSavePrefs<DataClass>("SAMPLE_CLASS_SAVE_KEY");
}

public enum EnumSample
{
	State1,
	State2,
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
