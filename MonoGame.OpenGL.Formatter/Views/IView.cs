﻿using MonoGame.OpenGL.Formatter.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.OpenGL.Formatter.Views
{
    public interface IView
    {
        public Guid ID { get; set; }
        public IWindow Parent { get; set; }

        public void ClearLayer(int layer);
        public void AddControl(int layer, IControl control);
        public void RemoveControl(int layer, IControl control);

        public void Initialize();
        public void Update(GameTime gameTime);
        public void OnUpdate(GameTime gameTime);
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public void SwitchView(IView screen);
    }
}
