using UnityEngine;

namespace Pspkurara.LocalSave
{

	/// <summary>
	/// <see cref="struct"/>を保存できるセーブスロット
	/// </summary>
	/// <typeparam name="TValue">保存したい値の型</typeparam>
	public sealed class StructSavePrefs<TValue> : SavePrefsBase<TValue> where TValue : struct
	{

		/// <summary>
		/// 生成
		/// </summary>
		/// <param name="key">保存キー</param>
		public StructSavePrefs(string key) : base(key) { }

		/// <summary>
		/// 生成
		/// </summary>
		/// <param name="key">保存キー</param>
		/// <param name="userSaveSystem">ここだけで有効なユーザー指定のセーブシステム</param>
		public StructSavePrefs(string key, ILocalSaveSystem userSaveSystem) : base(key, userSaveSystem) { }

		/// <summary>
		/// <see cref="default"/>扱いの文字列
		/// </summary>
		private const string Default = "";

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
				var result = SaveSystem.GetString(Key, defaultValue.Equals(default) ? JsonUtility.ToJson(defaultValue) : Default);
				if (result != Default)
				{
					return JsonUtility.FromJson<TValue>(result);
				}
				else
				{
					return defaultValue;
				}
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
			if (value.Equals(Default))
			{
				SaveSystem.SetString(Key, Default);
			}
			else
			{
				SaveSystem.SetString(Key, JsonUtility.ToJson(value));
			}
		}

	}

}
