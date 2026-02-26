using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.FormMatter.OpenGL.Helpers
{
	/// <summary>
	/// Helper class for various game methods
	/// </summary>
	public static class GameScreenHelper
	{
		/// <summary>
		/// Take a screenshot of the current rendered view.
		/// </summary>
		/// <param name="device"></param>
		/// <param name="game"></param>
		/// <returns></returns>
		public static Texture2D TakeScreenCap(GraphicsDevice device, Game game)
		{
			int w = device.PresentationParameters.BackBufferWidth;
			int h = device.PresentationParameters.BackBufferHeight;

			//force a frame to be drawn (otherwise back buffer is empty)
			game.RunOneFrame();
			//game.Draw(new GameTime());

			//pull the picture from the buffer
			int[] backBuffer = new int[w * h];
			device.GetBackBufferData(backBuffer);

			//copy into a texture
			Texture2D texture = new Texture2D(device, w, h, false, device.PresentationParameters.BackBufferFormat);
			texture.SetData(backBuffer);
			return texture;
		}
	}
}