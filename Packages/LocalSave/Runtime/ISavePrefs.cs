namespace Pspkurara.LocalSave
{

	/// <summary>
	/// <see cref="SavePrefsBase{TValue}"/>に関するインターフェイス
	/// </summary>
	public interface ISavePrefs
	{

		/// <summary>
		/// 自身の保存キー
		/// </summary>
		string Key { get; }

	}

}
