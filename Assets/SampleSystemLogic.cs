
using UnityEngine;

public class SampleSystemLogic : MonoBehaviour
{

	private void Start()
	{
		// primitive value
		Debug.Log(SampleSaves.SampleIntegerSaveData.GetValue());    // output: 0 (default value)

		Debug.Log(SampleSaves.SampleIntegerSaveData.GetValue(1));   // output: 1 (If a value is specified for the argument, it will be the default value)

		SampleSaves.SampleIntegerSaveData.SetValue(2);              // write: 2
		Debug.Log(SampleSaves.SampleIntegerSaveData.GetValue());    // output: 2 (Once written, the value is returned)

		// -----------

		// class value (ToString overrided)
		Debug.Log(SampleSaves.SampleClassSaveData.GetValue());      // output: null (default value)

		Debug.Log(SampleSaves.SampleClassSaveData.GetValue(new DataClass()));   // output: Value: 0. Length: 0 (If a value is specified for the argument, it will be the default value)

		Debug.Log(SampleSaves.SampleClassSaveData.GetValue(() => new DataClass())); // output: Value: 0. Length: 0 (Generation methods can be used as arguments to avoid wasting heap space when data is available)

		DataClass tempData = SampleSaves.SampleClassSaveData.GetValue(new DataClass());
		tempData.SampleValue = 10;
		SampleSaves.SampleClassSaveData.SetValue(tempData);         // write: simple value is default to 10
		Debug.Log(SampleSaves.SampleClassSaveData.GetValue());      // output: output: Value: 10. Length: 0 (Once written, the value is returned)
	}

}
