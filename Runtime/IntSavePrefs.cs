using UnityEngine;

namespace Pspkurara.LocalSave
{

	/// <summary>
	/// <see cref="int"/>を保存できるセーブスロット
	/// </summary>
	public sealed class IntSavePrefs : SavePrefsBase<int>
	{

		/// <summary>
		/// 生成
		/// </summary>
		/// <param name="key">保存キー</param>
		public IntSavePrefs(string key) : base(key) { }

		/// <summary>
		/// 生成
		/// </summary>
		/// <param name="key">保存キー</param>
		/// <param name="userSaveSystem">ここだけで有効なユーザー指定のセーブシステム</param>
		public IntSavePrefs(string key, ILocalSaveSystem userSaveSystem) : base(key, userSaveSystem) { }

		/// <summary>
		/// 値を取得する
		/// </summary>
		/// <param name="defaultValue">値がないときの標準値</param>
		/// <returns>
		/// 結果
		/// </returns>
		public override int GetValue(int defaultValue = default)
		{
			return SaveSystem.GetInt(Key, defaultValue);
		}

		/// <summary>
		/// 値を保存する
		/// </summary>
		/// <param name="value">値</param>
		public override void SetValue(int value)
		{
			SaveSystem.SetInt(Key, value);
		}

	}

}
