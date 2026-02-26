using FormMatter.OpenGL.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FormMatter.OpenGL.Textures
{
	/// <summary>
	/// <seealso cref="IContentController"/> implementation for handling textures
	/// </summary>
	public class TextureController : IContentController
	{
		/// <summary>
		/// Content manager for the game
		/// </summary>
		public ContentManager ContentManager { get; }

		private readonly Dictionary<Guid, TextureDefinition> _textures = new Dictionary<Guid, TextureDefinition>();
		private readonly Dictionary<Guid, TextureSetDefinition> _textureSets = new Dictionary<Guid, TextureSetDefinition>();
		private Texture2D? _noTexture;
		private TextureSetDefinition? _noTextureSet;

		/// <summary>
		/// Main controller
		/// </summary>
		/// <param name="contentManager"></param>
		public TextureController(ContentManager contentManager)
		{
			ContentManager = contentManager;
		}

		/// <summary>
		/// Load a given texture item
		/// </summary>
		/// <param name="item"></param>
		public void LoadTexture(TextureDefinition item)
		{
			if (_textures.ContainsKey(item.ID))
				_textures.Remove(item.ID);
			item.LoadContent(ContentManager);
			_textures.Add(item.ID, item);
		}

		/// <summary>
		/// Load a given texture set item
		/// </summary>
		/// <param name="item"></param>
		public void LoadTextureSet(TextureSetDefinition item)
		{
			if (_textureSets.ContainsKey(item.ID))
				_textureSets.Remove(item.ID);
			item.LoadContent(ContentManager);
			_textureSets.Add(item.ID, item);
		}

		/// <summary>
		/// Get some loaded texture based on its ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Texture2D GetTexture(Guid id)
		{
			if (_textures.ContainsKey(id))
				return _textures[id].GetLoadedContent();
			if (_noTexture == null)
				_noTexture = BasicTextures.GetBasicRectange(Color.HotPink);
			return _noTexture;
		}

		/// <summary>
		/// Check if a given texture ID exists
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool ContainsTexture(Guid id) => _textures.ContainsKey(id);
		/// <summary>
		/// Check if a given texture set ID exists
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool ContainsTextureSet(Guid id) => _textureSets.ContainsKey(id);

		/// <summary>
		/// Get some loaded texture set based on its ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public TextureSetDefinition GetTextureSet(Guid id)
		{
			if (_textureSets.ContainsKey(id))
				return _textureSets[id];
			if (_textures.ContainsKey(id))
			{
				var newSet = new TextureSetDefinition(id, 0, new List<string>(), false);
				newSet.SetContent(new List<Texture2D>() { _textures[id].GetLoadedContent() });
				_textureSets.Add(id, newSet);
				return newSet;
			}

			if (_noTextureSet == null)
			{
				if (_noTexture == null)
					_noTexture = BasicTextures.GetBasicRectange(Color.HotPink);
				_noTextureSet = new TextureSetDefinition(Guid.Empty, 9999, new List<string>(), false);
				_noTextureSet.SetContent(new List<Texture2D>() { _noTexture });
			}
			return _noTextureSet;
		}
	}
}