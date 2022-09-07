using UnityEngine;

namespace Pspkurara.LocalSave
{

	/// <summary>
	/// <see cref="PlayerPrefs"/>に対してローカルセーブを行うことが出来るコアロジック
	/// </summary>
	public sealed class PlayerPrefsLocalSaveSystem : ILocalSaveSystem
	{

		/// <summary>
		/// 一致するキーのデータが既に存在するか
		/// </summary>
		/// <param name="key">キー値</param>
		/// <returns></returns>
		public bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		/// <summary>
		/// 少数を取得する
		/// </summary>
		/// <param name="key">キー値</param>
		/// <param name="defaultValue">空の時の値</param>
		/// <returns>
		/// 取得した値
		/// </returns>
		public float GetFloat(string key, float defaultValue)
		{
			return PlayerPrefs.GetFloat(key, defaultValue);
		}

		/// <summary>
		/// 整数を取得する
		/// </summary>
		/// <param name="key">キー値</param>
		/// <param name="defaultValue">空の時の値</param>
		/// <returns>
		/// 取得した値
		/// </returns>
		public int GetInt(string key, int defaultValue)
		{
			return PlayerPrefs.GetInt(key, defaultValue);
		}

		/// <summary>
		/// 文字列を取得する
		/// </summary>
		/// <param name="key">キー値</param>
		/// <param name="defaultValue">空の時の値</param>
		/// <returns>
		/// 取得した値
		/// </returns>
		public string GetString(string key, string defaultValue)
		{
			return PlayerPrefs.GetString(key, defaultValue);
		}

		/// <summary>
		/// 少数を保存する
		/// </summary>
		/// <param name="key">キー値</param>
		/// <param name="value">値</param>
		public void SetFloat(string key, float value)
		{
			PlayerPrefs.SetFloat(key, value);
		}

		/// <summary>
		/// 整数を保存する
		/// </summary>
		/// <param name="key">キー値</param>
		/// <param name="value">値</param>
		public void SetInt(string key, int value)
		{
			PlayerPrefs.SetInt(key, value);
		}

		/// <summary>
		/// 文字列を保存する
		/// </summary>
		/// <param name="key">キー値</param>
		/// <param name="value">値</param>
		public void SetString(string key, string value)
		{
			PlayerPrefs.SetString(key, value);
		}

	}

}
