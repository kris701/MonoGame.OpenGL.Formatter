using FormMatter.OpenGL.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormMatter.OpenGL.Controls
{
	/// <summary>
	/// A control that displays a texture
	/// </summary>
	public class TileControl : BaseControl
	{
		/// <summary>
		/// The fill color of the control
		/// </summary>
		public Texture2D FillColor { get; set; } = BasicTextures.GetBasicRectange(Color.Transparent);

		/// <summary>
		/// Initialize the control
		/// </summary>
		public override void Initialize()
		{
			if (Width == 0)
				Width = FillColor.Width;
			if (Height == 0)
				Height = FillColor.Height;
			base.Initialize();
		}

		/// <summary>
		/// Draw the control
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="spriteBatch"></param>
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (!IsVisible)
				return;

			DrawTile(gameTime, spriteBatch, FillColor);
		}
	}
}