using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.FormMatter.OpenGL.Textures
{
	/// <summary>
	/// Structual definition of a texture
	/// </summary>
	public class TextureDefinition : LoadableContent<Texture2D>
	{
		/// <summary>
		/// Unique ID of this texture
		/// </summary>
		public Guid ID { get; set; }
		/// <summary>
		/// Path to the content
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="iD"></param>
		/// <param name="content"></param>
		/// <param name="isDefered"></param>
		public TextureDefinition(Guid iD, string content, bool isDefered) : base(isDefered)
		{
			ID = iD;
			Content = content;
		}

		/// <summary>
		/// Load the given texture.
		/// </summary>
		/// <param name="manager"></param>
		/// <returns></returns>
		public override Texture2D LoadMethod(ContentManager manager) => manager.Load<Texture2D>(Content);
	}
}