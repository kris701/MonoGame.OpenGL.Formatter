using Microsoft.Xna.Framework.Content;

namespace MonoGame.Formatter.OpenGL
{
	/// <summary>
	/// Base class for some externally loaded content, e.g. images and audio
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class LoadableContent<T>
	{
		/// <summary>
		/// If loading of the resource is defered or not.
		/// If it is defered, then the resource wont be read from the disk
		/// until it is needed. Otherwise, it will be loaded during normal
		/// game loading.
		/// </summary>
		public bool IsDefered { get; set; } = false;

		private ContentManager? _manager;
		private T? _loadedContent;

		/// <summary>
		/// Main constructor.
		/// </summary>
		/// <param name="isDefered"></param>
		protected LoadableContent(bool isDefered)
		{
			IsDefered = isDefered;
		}

		/// <summary>
		/// Get the loaded content as <typeparamref name="T"/>
		/// </summary>
		/// <returns>An instence of the loaded content.</returns>
		/// <exception cref="Exception"></exception>
		public T GetLoadedContent()
		{
			if (_loadedContent == null)
			{
				if (IsDefered && _manager != null)
					_loadedContent = LoadMethod(_manager);
				else
					throw new Exception("Content not loaded!");
			}
			return _loadedContent;
		}

		/// <summary>
		/// Load content using a content manager.
		/// If the resource is set to be defered, the content wont be loaded just yet.
		/// </summary>
		/// <param name="manager"></param>
		public void LoadContent(ContentManager manager)
		{
			if (!IsDefered)
				_loadedContent = LoadMethod(manager);
			else
				_manager = manager;
		}

		/// <summary>
		/// Actual loading method to implement
		/// </summary>
		/// <param name="manager"></param>
		/// <returns></returns>
		public abstract T LoadMethod(ContentManager manager);

		/// <summary>
		/// Manual overwrite to set content.
		/// </summary>
		/// <param name="content"></param>
		public void SetContent(T content) => _loadedContent = content;
	}
}