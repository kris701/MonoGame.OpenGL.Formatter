using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Formatter.OpenGL.BackgroundWorkers
{
	public abstract class BaseBackroundWorker : IBackgroundWorker
	{
		public virtual void Initialize()
		{
		}

		public virtual void Update(GameTime gameTime)
		{
		}

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
		}
	}
}