﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.OpenGL.Formatter.Audio;
using MonoGame.OpenGL.Formatter.BackgroundWorkers;
using MonoGame.OpenGL.Formatter.Fonts;
using MonoGame.OpenGL.Formatter.Textures;
using MonoGame.OpenGL.Formatter.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.OpenGL.Formatter
{
    public class BaseWindow : IWindow
    {
        public float XScale { get; private set; }
        public float YScale { get; private set; }

        public IView CurrentScreen { get; set; }
        public List<IBackgroundWorker> BackroundWorkers { get; set; } = new List<IBackgroundWorker>();
        public AudioController Audio { get; private set; }
        public TextureController Textures { get; private set; }
        public FontController Fonts { get; private set; }
        public bool IsActive { get; }

        public GraphicsDeviceManager Device { get; private set; }
        public ContentManager ContentManager { get; private set; }
        
        private Matrix _scaleMatrix;
        private SpriteBatch? _spriteBatch;

        public BaseWindow(ContentManager contentManager, GraphicsDeviceManager deviceManager)
        {
            ContentManager = contentManager;
            Device = deviceManager;
        }

        public void InitializeWindow()
        {
            Audio = new AudioController(ContentManager);
            Textures = new TextureController(ContentManager);
            Fonts = new FontController(ContentManager);
            foreach (var worker in BackroundWorkers)
                worker.Initialize();
        }

        public void LoadContentWindow()
        {
            _spriteBatch = new SpriteBatch(Device.GraphicsDevice);
        }

        public void DrawWindow(GameTime gameTime)
        {
            Device.GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch!.Begin(transformMatrix: _scaleMatrix);
            CurrentScreen.Draw(gameTime, _spriteBatch);
            foreach (var worker in BackroundWorkers)
                worker.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
        }

        public void UpdateScale()
        {
            XScale = Device.PreferredBackBufferWidth / (float)IWindow.BaseScreenSize.X;
            YScale = Device.PreferredBackBufferHeight / (float)IWindow.BaseScreenSize.Y;
            _scaleMatrix = Matrix.CreateScale(XScale, YScale, 1.0f);
        }
    }
}