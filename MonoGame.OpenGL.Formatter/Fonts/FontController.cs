using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.OpenGL.Formatter.Fonts
{
    /// <summary>
    /// <seealso cref="IController"/> implementation for handling fonts
    /// </summary>
    public class FontController : IController
    {
        public ContentManager ContentManager { get; }
        public string ContentDir { get; }

        public FontController(ContentManager contentManager, string contentDir)
        {
            ContentManager = contentManager;
            ContentDir = contentDir;
        }

        private readonly Dictionary<int, SpriteFont> _cache = new Dictionary<int, SpriteFont>();

        public SpriteFont GetFont(int fontSize)
        {
            if (_cache.ContainsKey(fontSize))
                return _cache[fontSize];

            var font = ContentManager.Load<SpriteFont>($"{ContentDir}{fontSize}");
            _cache.Add(fontSize, font);
            return font;
        }
    }
}
