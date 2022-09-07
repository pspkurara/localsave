using UnityEngine;
using System;

namespace Pspkurara.LocalSave
{

	/// <summary>
	/// <see cref="enum"/>を保存できるセーブスロット
	/// </summary>
	/// <typeparam name="TValue">保存する値の型</typeparam>
	public sealed class EnumSavePrefs<TValue> : SavePrefsBase<TValue> where TValue : Enum
	{

		/// <summary>
		/// 生成
		/// </summary>
		/// <param name="key">保存キー</param>
		public EnumSavePrefs(string key) : base(key) { }

		/// <summary>
		/// 生成
		/// </summary>
		/// <param name="key">保存キー</param>
		/// <param name="userSaveSystem">ここだけで有効なユーザー指定のセーブシステム</param>
		public EnumSavePrefs(string key, ILocalSaveSystem userSaveSystem) : base(key, userSaveSystem) { }

		/// <summary>
		/// 値を取得する
		/// </summary>
		/// <param name="defaultValue">値がないときの標準値</param>
		/// <returns>
		/// 結果
		/// </returns>
		public override TValue GetValue(TValue defaultValue = default)
		{
			try
			{
				return (TValue)Enum.ToObject(typeof(TValue), SaveSystem.GetInt(Key, Convert.ToInt32(defaultValue)));
			}
			catch (System.Exception e)
			{
				Debug.LogError(e);
				return defaultValue;
			}
		}

		/// <summary>
		/// 値を保存する
		/// </summary>
		/// <param name="value">値</param>
		public override void SetValue(TValue value)
		{
			SaveSystem.SetInt(Key, Convert.ToInt32(value));
		}

	}

}
