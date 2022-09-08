using UnityEngine;
using Pspkurara.LocalSave;

public class SampleSavePrefsBase : MonoBehaviour
{

	private static LongSavePrefs LongSave = new LongSavePrefs("LONG_SAVE_KEY");

	// Start is called before the first frame update
	void Start()
	{
		Debug.Log(LongSave.GetValue());     // output: 0 (default value)

		Debug.Log(LongSave.GetValue(1));    // output: 1

		LongSave.SetValue(long.MaxValue);   // write value.
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
