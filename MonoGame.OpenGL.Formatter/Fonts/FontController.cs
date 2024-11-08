﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.OpenGL.Formatter.Fonts
{
    /// <summary>
    /// <seealso cref="IController"/> implementation for handling fonts
    /// </summary>
    public class FontController : IController
    {
        public ContentManager ContentManager { get; }
        private readonly Dictionary<Guid, FontDefinition> _fonts = new Dictionary<Guid, FontDefinition>();

        public FontController(ContentManager contentManager)
        {
            ContentManager = contentManager;
        }

        public void LoadFont(FontDefinition item)
        {
            if (_fonts.ContainsKey(item.ID))
                _fonts.Remove(item.ID);
            item.LoadContent(ContentManager);
            _fonts.Add(item.ID, item);
        }

        public SpriteFont GetFont(Guid id)
        {
            if (!_fonts.ContainsKey(id))
                throw new Exception($"Font with ID '{id}' has not been loaded!");
            return _fonts[id].GetLoadedContent();
        }
    }
}