using UnityEngine;
using System;

namespace Pspkurara.LocalSave
{

	/// <summary>
	/// 外部から読み出し書き込みロジックを指定できるセーブスロット
	/// </summary>
	/// <typeparam name="TValue">保存する値の型</typeparam>
	public sealed class ExternalSavePrefs<TValue> : SavePrefsBase<TValue>
	{

		/// <summary>
		/// 生成
		/// </summary>
		/// <param name="key">保存キー</param>
		/// <param name="getter">値を取得するメソッド</param>
		/// <param name="setter">値を保存するメソッド</param>
		public ExternalSavePrefs(string key, Func<string, TValue, TValue> getter, Action<string, TValue> setter) : base (key)
		{
			this.getter = getter;
			this.setter = setter;
		}

		private Func<string, TValue, TValue> getter;

		private Action<string, TValue> setter;

		/// <summary>
		/// 値を取得する
		/// </summary>
		/// <param name="defaultValue">値がないときの標準値</param>
		/// <returns>
		/// 結果
		/// </returns>
		public override TValue GetValue(TValue defaultValue = default)
		{
			if (getter == null)
			{
				Debug.LogError($"getter is empty. {GetType()} / {Key}");
				return defaultValue;
			}
			return getter.Invoke(Key, defaultValue);
		}

		/// <summary>
		/// 値を保存する
		/// </summary>
		/// <param name="value">値</param>
		public override void SetValue(TValue value)
		{
			if (setter == null)
			{
				Debug.LogError($"setter is empty. {GetType()} / {Key}");
				return;
			}
			setter.Invoke(Key, value);
		}

	}

}
