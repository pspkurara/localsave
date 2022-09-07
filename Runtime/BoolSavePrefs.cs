using UnityEngine;

namespace Pspkurara.LocalSave
{

	/// <summary>
	/// <see cref="bool"/>を保存できるセーブスロット
	/// </summary>
	public sealed class BoolSavePrefs : SavePrefsBase<bool>
	{

		/// <summary>
		/// 生成
		/// </summary>
		/// <param name="key">保存キー</param>
		public BoolSavePrefs(string key) : base(key) { }

		/// <summary>
		/// 生成
		/// </summary>
		/// <param name="key">保存キー</param>
		/// <param name="userSaveSystem">ここだけで有効なユーザー指定のセーブシステム</param>
		public BoolSavePrefs(string key, ILocalSaveSystem userSaveSystem) : base(key, userSaveSystem) { }

		/// <summary>
		/// int上でのtrue
		/// </summary>
		private const int True = 1;

		/// <summary>
		/// int上でのfalse
		/// </summary>
		private const int False = 0;

		/// <summary>
		/// 値を取得する
		/// </summary>
		/// <param name="defaultValue">値がないときの標準値</param>
		/// <returns>
		/// 結果
		/// </returns>
		public override bool GetValue(bool defaultValue = default)
		{
			return SaveSystem.GetInt(Key, defaultValue ? True : False) == True;
		}

		/// <summary>
		/// 値を保存する
		/// </summary>
		/// <param name="value">値</param>
		public override void SetValue(bool value)
		{
			SaveSystem.SetInt(Key, value ? True : False);
		}

	}

}
