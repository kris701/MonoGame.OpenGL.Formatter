using Microsoft.Xna.Framework.Content;

namespace MonoGame.OpenGL.Formatter
{
    /// <summary>
    /// Base content controller interface.
    /// </summary>
    public interface IContentController
    {
        /// <summary>
        /// Content manager for the game.
        /// </summary>
        public ContentManager ContentManager { get; }
    }
}