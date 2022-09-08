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
