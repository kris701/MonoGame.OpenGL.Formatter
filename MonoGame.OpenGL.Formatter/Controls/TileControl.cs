﻿using MonoGame.OpenGL.Formatter.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.OpenGL.Formatter.Controls
{
    public class TileControl : BaseControl
    {
        public Texture2D FillColor { get; set; } = BasicTextures.GetBasicRectange(Color.Transparent);

        public override void Initialize()
        {
            if (Width == 0)
                Width = FillColor.Width;
            if (Height == 0)
                Height = FillColor.Height;
            base.Initialize();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!IsVisible)
                return;

            DrawTile(gameTime, spriteBatch, FillColor);
        }
    }
}
