using FormMatter.OpenGL.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormMatter.OpenGL.Controls
{
	/// <summary>
	/// A control to draw a line between two points
	/// </summary>
	public class LineControl : BaseControl
	{
		/// <summary>
		/// Thickness of the line
		/// </summary>
		public float Thickness { get; set; }
		/// <summary>
		/// Color of the line
		/// </summary>
		public Texture2D Stroke { get; set; } = BasicTextures.GetBasicRectange(Color.Red);
		/// <summary>
		/// The X position of the 2nd point
		/// </summary>
		public float X2 { get; set; }
		/// <summary>
		/// The Y position of the 2nd point
		/// </summary>
		public float Y2 { get; set; }

		/// <summary>
		/// Draw the line
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="spriteBatch"></param>
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.DrawLine(Stroke, new Vector2(X, Y), new Vector2(X2, Y2), Thickness, Alpha);
		}
	}
}