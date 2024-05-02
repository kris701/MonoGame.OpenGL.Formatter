﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.OpenGL.Formatter.Controls.Elements;
using MonoGame.OpenGL.Formatter.Helpers;

namespace MonoGame.OpenGL.Formatter.Controls
{
    public class ButtonControl : LabelControl
    {
        public delegate void ClickedHandler(ButtonControl parent);
        public event ClickedHandler? Clicked;

        public IWindow Parent { get; set; }
        public Guid ClickSound
        {
            get => _clickSoundElement.SoundEffect;
            set => _clickSoundElement.SoundEffect = value;
        }
        public Texture2D FillClickedColor { get; set; } = BasicTextures.GetBasicRectange(Color.Gray);
        public Texture2D FillDisabledColor { get; set; } = BasicTextures.GetBasicRectange(Color.DarkGray);
        public bool IsEnabled { get; set; } = true;

        private readonly SoundEffectElement _clickSoundElement;
        private bool _holding = false;
        private bool _blocked = false;

        public ButtonControl(IWindow parent, ClickedHandler? clicked = null)
        {
            Parent = parent;
            Clicked += clicked;
            _clickSoundElement = new SoundEffectElement(parent);
        }

        public void DoClick()
        {
            if (!IsEnabled || !IsVisible)
                return;
            _holding = true;
            _clickSoundElement.Trigger();
            Clicked?.Invoke(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (IsEnabled && IsVisible && Parent.IsActive && Clicked != null)
            {
                var mouseState = Mouse.GetState();
                var translatedPos = InputHelper.GetRelativePosition(Parent.XScale, Parent.YScale);
                if (!_blocked && (translatedPos.X > X && translatedPos.X < X + Width &&
                    translatedPos.Y > Y && translatedPos.Y < Y + Height))
                {
                    if (!_holding && mouseState.LeftButton == ButtonState.Pressed)
                        _holding = true;
                    else if (_holding && mouseState.LeftButton == ButtonState.Released)
                    {
                        _clickSoundElement.Trigger();
                        Clicked?.Invoke(this);
                        _holding = false;
                    }
                }
                else
                {
                    if (_holding && mouseState.LeftButton == ButtonState.Released)
                        _holding = false;
                    if (mouseState.LeftButton == ButtonState.Pressed)
                        _blocked = true;
                    else
                        _blocked = false;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!IsVisible)
                return;

            base.Draw(gameTime, spriteBatch);
            if (_holding)
                DrawTile(gameTime, spriteBatch, FillClickedColor);
            if (!IsEnabled)
                DrawTile(gameTime, spriteBatch, FillDisabledColor);
        }
    }
}
