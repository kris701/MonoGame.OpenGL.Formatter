using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.OpenGL.Formatter.BackgroundWorkers
{
    /// <summary>
    /// An interface for background workers.
    /// </summary>
    public interface IBackgroundWorker
    {
        public void Initialize(); // Constructor level initialization

        public void Update(GameTime gameTime); // Update each frame

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch); // Draw each frame
    }
}