using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Formatter.OpenGL.Helpers
{
	/// <summary>
	/// Helper class to make drawing with <seealso cref="SpriteBatch"/>es easier.
	/// </summary>
	public static class SpriteBatchHelper
	{
		/// <summary>
		/// Fill the entire screen with some texture
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="texture"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="alpha"></param>
		public static void FillScreen(this SpriteBatch spriteBatch, Texture2D texture, int width, int height, int alpha = 255)
		{
			spriteBatch.Draw(texture, new Rectangle(0, 0, width, height), new Color(255, 255, 255, alpha));
		}

		/// <summary>
		/// Draw a line
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="texture"></param>
		/// <param name="point1"></param>
		/// <param name="point2"></param>
		/// <param name="thickness"></param>
		/// <param name="alpha"></param>
		public static void DrawLine(this SpriteBatch spriteBatch, Texture2D texture, Vector2 point1, Vector2 point2, float thickness = 1f, int alpha = 255)
		{
			var distance = Vector2.Distance(point1, point2);
			var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
			DrawLine(spriteBatch, texture, point1, distance, angle, thickness, alpha);
		}

		/// <summary>
		/// Draw a line
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="texture"></param>
		/// <param name="point"></param>
		/// <param name="length"></param>
		/// <param name="angle"></param>
		/// <param name="thickness"></param>
		/// <param name="alpha"></param>
		public static void DrawLine(this SpriteBatch spriteBatch, Texture2D texture, Vector2 point, float length, float angle, float thickness = 1f, int alpha = 255)
		{
			var origin = new Vector2(0f, 0.5f);
			var scale = new Vector2(length, thickness);
			spriteBatch.Draw(texture, point, null, new Color(255, 255, 255, alpha), angle, origin, scale, SpriteEffects.None, 0);
		}
	}
}