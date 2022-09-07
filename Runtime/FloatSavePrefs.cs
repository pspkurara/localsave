using UnityEngine;

namespace Pspkurara.LocalSave
{

	/// <summary>
	/// <see cref="float"/>を保存できるセーブスロット
	/// </summary>
	public sealed class FloatSavePrefs : SavePrefsBase<float>
	{

		/// <summary>
		/// 生成
		/// </summary>
		/// <param name="key">保存キー</param>
		public FloatSavePrefs(string key) : base(key) { }

		/// <summary>
		/// 生成
		/// </summary>
		/// <param name="key">保存キー</param>
		/// <param name="userSaveSystem">ここだけで有効なユーザー指定のセーブシステム</param>
		public FloatSavePrefs(string key, ILocalSaveSystem userSaveSystem) : base(key, userSaveSystem) { }

		/// <summary>
		/// 値を取得する
		/// </summary>
		/// <param name="defaultValue">値がないときの標準値</param>
		/// <returns>
		/// 結果
		/// </returns>
		public override float GetValue(float defaultValue = default)
		{
			return SaveSystem.GetFloat(Key, defaultValue);
		}

		/// <summary>
		/// 値を保存する
		/// </summary>
		/// <param name="value">値</param>
		public override void SetValue(float value)
		{
			SaveSystem.SetFloat(Key, value);
		}

	}

}
