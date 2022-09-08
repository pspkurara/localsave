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

		Debug.Log(ExtSave.GetValue());  // output: 0 (default value)

		Debug.Log(ExtSave.GetValue(1)); // output: 1

		ExtSave.SetValue(2);            // write value.
		Debug.Log(ExtSave.GetValue());  // output : 2
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
