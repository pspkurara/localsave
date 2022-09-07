using System;

namespace Pspkurara.LocalSave
{

	/// <summary>
	/// ローカルセーブの設定
	/// </summary>
	public static class LocalSaveSettings
	{

		/// <summary>
		/// 標準のセーブシステム
		/// </summary>
		private static ILocalSaveSystem m_SaveSystem = null;

		/// <summary>
		/// 標準のセーブシステム
		/// 何もされなければ<see cref="PlayerPrefsLocalSaveSystem"/>を使う
		/// </summary>
		public static ILocalSaveSystem SaveSystem
		{
			get
			{
				if (m_SaveSystem == null) m_SaveSystem = new PlayerPrefsLocalSaveSystem();
				return m_SaveSystem;
			}
			set
			{
				m_SaveSystem = value;
			}
		}

	}

}
