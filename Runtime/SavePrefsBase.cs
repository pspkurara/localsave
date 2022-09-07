namespace Pspkurara.LocalSave
{

	/// <summary>
	/// セーブスロットの基本
	/// </summary>
	/// <typeparam name="TValue">保存する値の型</typeparam>
	public abstract class SavePrefsBase<TValue> : ISavePrefs
	{

		/// <summary>
		/// 生成
		/// </summary>
		/// <param name="key">保存キー</param>
		public SavePrefsBase(string key) : this(key, null) { }

		/// <summary>
		/// 生成
		/// </summary>
		/// <param name="key">保存キー</param>
		/// <param name="userSaveSystem">ここだけで有効なユーザー指定のセーブシステム</param>
		public SavePrefsBase(string key, ILocalSaveSystem userSaveSystem)
		{
			this.Key = key;
			this.userSaveSystem = userSaveSystem;
		}

		/// <summary>
		/// 自身の保存キー
		/// </summary>
		public string Key { get; private set; }

		/// <summary>
		/// 既にデータが保存済みかどうか
		/// </summary>
		/// <remarks>
		/// 真の場合：保存されている
		/// </remarks>
		public bool HasKey { get { return SaveSystem.HasKey(Key); } }

		/// <summary>
		/// ユーザー指定セーブシステム
		/// </summary>
		private ILocalSaveSystem userSaveSystem;

		/// <summary>
		/// セーブシステム
		/// </summary>
		protected ILocalSaveSystem SaveSystem { get { return userSaveSystem != null ? userSaveSystem : LocalSaveSettings.SaveSystem; } }

		/// <summary>
		/// 値を取得する
		/// </summary>
		/// <param name="defaultValue">値がないときの標準値</param>
		/// <returns>結果</returns>
		public abstract TValue GetValue(TValue defaultValue = default);

		/// <summary>
		/// 値を取得する
		/// </summary>
		/// <param name="defaultGetter">値がないときの標準値を取得する関数</param>
		/// <returns>結果</returns>
		public TValue GetValue(System.Func<TValue> defaultGetter)
		{
			if (!SaveSystem.HasKey(Key)) return GetValue(defaultGetter.Invoke());
			return GetValue((TValue)default);
		}

		/// <summary>
		/// 値を保存する
		/// </summary>
		/// <param name="value">値</param>
		public abstract void SetValue(TValue value);

	}

}
