﻿using MonoGame.OpenGL.Formatter.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.OpenGL.Formatter.Controls
{
    public class BorderControl : TileControl
    {
        public Texture2D BorderBrush { get; set; } = BasicTextures.GetBasicRectange(Color.Black);
        public float Thickness { get; set; } = 2;
        public IControl Child { get; set; }

        public BorderControl(IControl child)
        {
            Child = child;
        }

        public override void Initialize()
        {
            Child.Initialize();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Child.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!IsVisible)
                return;

            Child.Draw(gameTime, spriteBatch);
            spriteBatch.DrawLine(BorderBrush, new Vector2(Child.X - Thickness / 2, Child.Y), new Vector2(Child.X + Child.Width + Thickness / 2, Child.Y), Thickness, Alpha);
            spriteBatch.DrawLine(BorderBrush, new Vector2(Child.X, Child.Y), new Vector2(Child.X, Child.Y + Child.Height), Thickness, Alpha);
            spriteBatch.DrawLine(BorderBrush, new Vector2(Child.X + Child.Width, Child.Y), new Vector2(Child.X + Child.Width, Child.Y + Child.Height), Thickness, Alpha);
            spriteBatch.DrawLine(BorderBrush, new Vector2(Child.X - Thickness / 2, Child.Y + Child.Height), new Vector2(Child.X + Child.Width + Thickness / 2, Child.Y + Child.Height), Thickness, Alpha);
        }
    }
}
