using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.FormMatter.OpenGL.Fonts
{
	/// <summary>
	/// Structural definition of a font
	/// </summary>
	public class FontDefinition : LoadableContent<SpriteFont>
	{
		/// <summary>
		/// Unique ID of this font
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
		public FontDefinition(Guid iD, string content, bool isDefered) : base(isDefered)
		{
			ID = iD;
			Content = content;
		}

		/// <summary>
		/// Load the given font.
		/// </summary>
		/// <param name="manager"></param>
		/// <returns></returns>
		public override SpriteFont LoadMethod(ContentManager manager) => manager.Load<SpriteFont>(Content);
	}
}