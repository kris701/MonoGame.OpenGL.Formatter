using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FormMatter.OpenGL.Fonts
{
	/// <summary>
	/// <seealso cref="IContentController"/> implementation for handling fonts
	/// </summary>
	public class FontController : IContentController
	{
		/// <summary>
		/// Content manager for the game
		/// </summary>
		public ContentManager ContentManager { get; }

		private readonly Dictionary<Guid, FontDefinition> _fonts = new Dictionary<Guid, FontDefinition>();

		/// <summary>
		/// Main controller
		/// </summary>
		/// <param name="contentManager"></param>
		public FontController(ContentManager contentManager)
		{
			ContentManager = contentManager;
		}

		/// <summary>
		/// Load a font
		/// </summary>
		/// <param name="item"></param>
		public void LoadFont(FontDefinition item)
		{
			if (_fonts.ContainsKey(item.ID))
				_fonts.Remove(item.ID);
			item.LoadContent(ContentManager);
			_fonts.Add(item.ID, item);
		}

		/// <summary>
		/// Get a font based on a unique ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public SpriteFont GetFont(Guid id)
		{
			if (!_fonts.ContainsKey(id))
				throw new Exception($"Font with ID '{id}' has not been loaded!");
			return _fonts[id].GetLoadedContent();
		}

		/// <summary>
		/// Check if a given font ID exists
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool ContainsFont(Guid id) => _fonts.ContainsKey(id);
	}
}