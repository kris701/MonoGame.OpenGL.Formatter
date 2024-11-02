﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.OpenGL.Formatter.Helpers;

namespace MonoGame.OpenGL.Formatter.Textures
{
    /// <summary>
    /// <seealso cref="IController"/> implementation for handling textures
    /// </summary>
    public class TextureController : IController
    {
        public ContentManager ContentManager { get; }

        private readonly Dictionary<Guid, TextureDefinition> _textures = new Dictionary<Guid, TextureDefinition>();
        private readonly Dictionary<Guid, TextureSetDefinition> _textureSets = new Dictionary<Guid, TextureSetDefinition>();
        private Texture2D? _noTexture;
        private TextureSetDefinition? _noTextureSet;

        public TextureController(ContentManager contentManager)
        {
            ContentManager = contentManager;
        }

        public void LoadTexture(TextureDefinition item)
        {
            if (_textures.ContainsKey(item.ID))
                _textures.Remove(item.ID);
            item.LoadContent(ContentManager);
            _textures.Add(item.ID, item);
        }

        public void LoadTextureSet(TextureSetDefinition item)
        {
            if (_textureSets.ContainsKey(item.ID))
                _textureSets.Remove(item.ID);
            item.LoadContent(ContentManager);
            _textureSets.Add(item.ID, item);
        }

        public Texture2D GetTexture(Guid id)
        {
            if (_textures.ContainsKey(id))
                return _textures[id].GetLoadedContent();
            if (_noTexture == null)
                _noTexture = BasicTextures.GetBasicRectange(Color.HotPink);
            return _noTexture;
        }

        public TextureSetDefinition GetTextureSet(Guid id)
        {
            if (_textureSets.ContainsKey(id))
                return _textureSets[id];
            if (_textures.ContainsKey(id))
            {
                var newSet = new TextureSetDefinition(id, 0, new List<string>(), false);
                newSet.SetContent(new List<Texture2D>() { _textures[id].GetLoadedContent() });
                _textureSets.Add(id, newSet);
                return newSet;
            }

            if (_noTextureSet == null)
            {
                if (_noTexture == null)
                    _noTexture = BasicTextures.GetBasicRectange(Color.HotPink);
                _noTextureSet = new TextureSetDefinition(Guid.Empty, 9999, new List<string>(), false);
                _noTextureSet.SetContent(new List<Texture2D>() { _noTexture });
            }
            return _noTextureSet;
        }
    }
}