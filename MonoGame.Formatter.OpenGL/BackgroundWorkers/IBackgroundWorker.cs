using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Formatter.OpenGL.BackgroundWorkers
{
	/// <summary>
	/// An interface for background workers.
	/// </summary>
	public interface IBackgroundWorker
	{
		/// <summary>
		/// Initialize the worker
		/// </summary>
		public void Initialize();

		/// <summary>
		/// Update the worker
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime);

		/// <summary>
		/// Draw the worker
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="spriteBatch"></param>
		public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
	}
}