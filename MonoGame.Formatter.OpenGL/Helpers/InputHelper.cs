using Microsoft.Xna.Framework.Input;

namespace MonoGame.Formatter.OpenGL.Helpers
{
	/// <summary>
	/// Helper methods for mouse input
	/// </summary>
	public static class InputHelper
	{
		/// <summary>
		/// Get the relative position of the mouse based on a scale
		/// </summary>
		/// <param name="scale"></param>
		/// <returns></returns>
		public static FloatPoint GetRelativePosition(float scale)
		{
			var mouseState = Mouse.GetState();
			var translatedPos = new FloatPoint(mouseState.X / scale, mouseState.Y / scale);
			return translatedPos;
		}

		/// <summary>
		/// Get the relative position of the mouse based on a scale
		/// </summary>
		/// <param name="scaleX"></param>
		/// <param name="scaleY"></param>
		/// <returns></returns>
		public static FloatPoint GetRelativePosition(float scaleX, float scaleY)
		{
			var mouseState = Mouse.GetState();
			var translatedPos = new FloatPoint(mouseState.X / scaleX, mouseState.Y / scaleY);
			return translatedPos;
		}
	}
}