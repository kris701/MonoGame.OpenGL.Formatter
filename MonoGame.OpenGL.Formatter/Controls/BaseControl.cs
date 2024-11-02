﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.OpenGL.Formatter.Controls
{
    public abstract class BaseControl : IControl
    {
        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.None;
        public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.None;

        public float Rotation { get; set; } = 0;
        public bool IsVisible { get; set; } = true;
        public bool IsInitialized { get; set; } = false;
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        private Color _alphaColor = new Color(255, 255, 255, 255);
        private int _alpha = 255;

        public int Alpha
        {
            get
            {
                return _alpha;
            }
            set
            {
                _alpha = value;
                _alphaColor = new Color(value, value, value, value);
            }
        }

        public object? Tag { get; set; }
        private bool _usesViewPort = false;
        private Rectangle _actualViewPort;
        private Rectangle _viewPort;

        public Rectangle ViewPort
        {
            get
            {
                return _viewPort;
            }
            set
            {
                var copy = new Rectangle(value.X, value.Y, value.Width, value.Height);
                _viewPort = copy;
                _usesViewPort = true;
            }
        }

        public void CalculateViewPort()
        {
            var viewPortX = 0f;
            var viewPortY = 0f;
            var viewPortWidth = Width;
            var viewPortHeight = Height;
            if (Width > _viewPort.Width)
                viewPortWidth = _viewPort.Width;
            if (Height > _viewPort.Height)
                viewPortHeight = _viewPort.Height;
            if (X < _viewPort.X)
            {
                viewPortX -= X - _viewPort.X;
                if (X + Width < _viewPort.X + _viewPort.Width)
                    viewPortWidth -= viewPortWidth - (X + Width - _viewPort.X);
            }
            if (Y < _viewPort.Y)
            {
                viewPortY -= Y - _viewPort.Y;
                if (Y + Height < _viewPort.Y + _viewPort.Height)
                    viewPortHeight -= viewPortHeight - (Y + Width - _viewPort.Y);
            }
            if (X + viewPortWidth > _viewPort.X + _viewPort.Width)
                viewPortWidth -= (X + viewPortWidth) - (_viewPort.X + _viewPort.Width);
            if (Y + viewPortHeight > _viewPort.Y + _viewPort.Height)
                viewPortHeight -= (Y + viewPortHeight) - (_viewPort.Y + _viewPort.Height);

            _actualViewPort = new Rectangle((int)viewPortX, (int)viewPortY, (int)viewPortWidth, (int)viewPortHeight);
        }

        internal void ReAlign()
        {
            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left: X = 0; break;
                case HorizontalAlignment.Right: X = IWindow.BaseScreenSize.X - Width; break;
                case HorizontalAlignment.Middle: X = IWindow.BaseScreenSize.X / 2 - Width / 2; break;
            }
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Top: Y = 0; break;
                case VerticalAlignment.Bottom: Y = IWindow.BaseScreenSize.Y - Height; break;
                case VerticalAlignment.Middle: Y = IWindow.BaseScreenSize.Y / 2 - Height / 2; break;
            }
        }

        public virtual void Initialize()
        {
            ReAlign();
            if (_usesViewPort)
                CalculateViewPort();
            IsInitialized = true;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public void OffsetFrom(IControl parent)
        {
            X += parent.X;
            Y += parent.Y;
        }

        internal void DrawTile(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture)
        {
            if (texture.Width == 1 && texture.Height == 1)
            {
                if (_usesViewPort)
                    spriteBatch.Draw(
                        texture,
                        new Vector2(X + Width / 2, Y + Height / 2),
                        _actualViewPort,
                        _alphaColor,
                        0,
                        new Vector2(Width / 2, Height / 2),
                        1,
                        SpriteEffects.None,
                        0);
                else
                    spriteBatch.Draw(
                        texture,
                        new Vector2(X + Width / 2, Y + Height / 2),
                        new Rectangle(0, 0, (int)Width, (int)Height),
                        _alphaColor,
                        Rotation,
                        new Vector2(Width / 2, Height / 2),
                        1,
                        SpriteEffects.None,
                        0);
            }
            else
            {
                var xFit = Width / texture.Width;
                var yFit = Height / texture.Height;

                if (_usesViewPort)
                {
                    var scaledViewPort = new Rectangle(
                        (int)(_actualViewPort.X / xFit),
                        (int)(_actualViewPort.Y / yFit),
                        (int)(_actualViewPort.Width / xFit),
                        (int)(_actualViewPort.Height / yFit));
                    spriteBatch.Draw(
                        texture,
                        new Vector2(X + Width / 2, Y + Height / 2),
                        scaledViewPort,
                        _alphaColor,
                        0,
                        new Vector2(texture.Width / 2 - scaledViewPort.X, texture.Height / 2 - scaledViewPort.Y),
                        new Vector2(xFit, yFit),
                        SpriteEffects.None,
                        0);
                }
                else
                    spriteBatch.Draw(
                        texture,
                        new Vector2(X + Width / 2, Y + Height / 2),
                        null,
                        _alphaColor,
                        Rotation,
                        new Vector2(texture.Width / 2, texture.Height / 2),
                        new Vector2(xFit, yFit),
                        SpriteEffects.None,
                        0);
            }
        }
    }
}