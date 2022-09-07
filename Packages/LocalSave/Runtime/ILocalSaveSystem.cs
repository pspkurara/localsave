using UnityEngine;

namespace Pspkurara.LocalSave
{

	/// <summary>
	/// ローカルセーブを行うコアロジック部分を独自に差し替える時に使う
	/// 継承して使用
	/// </summary>
	public interface ILocalSaveSystem
	{

		/// <summary>
		/// 一致するキーのデータが既に存在するか
		/// </summary>
		/// <param name="key">キー値</param>
		/// <returns>
		/// 
		/// </returns>
		bool HasKey(string key);

		/// <summary>
		/// 少数を取得する
		/// </summary>
		/// <param name="key">キー値</param>
		/// <param name="defaultValue">空の時の値</param>
		/// <returns>取得した値</returns>
		float GetFloat(string key, float defaultValue);

		/// <summary>
		/// 少数を保存する
		/// </summary>
		/// <param name="key">キー値</param>
		/// <param name="value">値</param>
		void SetFloat(string key, float value);

		/// <summary>
		/// 文字列を取得する
		/// </summary>
		/// <param name="key">キー値</param>
		/// <param name="defaultValue">空の時の値</param>
		/// <returns>取得した値</returns>
		string GetString(string key, string defaultValue);

		/// <summary>
		/// 文字列を保存する
		/// </summary>
		/// <param name="key">キー値</param>
		/// <param name="value">値</param>
		void SetString(string key, string value);

		/// <summary>
		/// 整数を取得する
		/// </summary>
		/// <param name="key">キー値</param>
		/// <param name="defaultValue">空の時の値</param>
		/// <returns>取得した値</returns>
		int GetInt(string key, int defaultValue);

		/// <summary>
		/// 整数を保存する
		/// </summary>
		/// <param name="key">キー値</param>
		/// <param name="value">値</param>
		void SetInt(string key, int value);

	}

}
