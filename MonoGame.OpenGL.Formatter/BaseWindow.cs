using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.OpenGL.Formatter.Audio;
using MonoGame.OpenGL.Formatter.BackgroundWorkers;
using MonoGame.OpenGL.Formatter.Fonts;
using MonoGame.OpenGL.Formatter.Textures;
using MonoGame.OpenGL.Formatter.Views;
using System.Reflection;

namespace MonoGame.OpenGL.Formatter
{
    /// <summary>
    /// A base implementation of the <seealso cref="IWindow"/> interface and <seealso cref="Game"/> class.
    /// </summary>
    public class BaseWindow : Game, IWindow
    {
        public float XScale { get; private set; } = 1;
        public float YScale { get; private set; } = 1;

        public IView CurrentScreen { get; set; }
        public List<IBackgroundWorker> BackroundWorkers { get; set; } = new List<IBackgroundWorker>();
        public AudioController Audio { get; private set; }
        public TextureController Textures { get; private set; }
        public FontController Fonts { get; private set; }
        public new bool IsActive { get => base.IsActive; }

        public GraphicsDeviceManager Device { get; }

        private Matrix _scaleMatrix;
        private SpriteBatch? _spriteBatch;
        private readonly string? _title;

        public BaseWindow() : base()
        {
            Device = new GraphicsDeviceManager(this);
            UpdateScale();
        }

        public BaseWindow(string title) : this()
        {
            _title = title;
        }

        protected override void Initialize()
        {
            base.Initialize();

            if (_title != null)
            {
                var thisVersion = Assembly.GetEntryAssembly()?.GetName().Version!;
                var thisVersionStr = $"v{thisVersion.Major}.{thisVersion.Minor}.{thisVersion.Build}";
                Window.Title = $"{_title} {thisVersionStr}";
            }

            Audio = new AudioController(Content);
            Textures = new TextureController(Content);
            Fonts = new FontController(Content);

            foreach (var worker in BackroundWorkers)
                worker.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
            foreach (var worker in BackroundWorkers)
                worker.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch!.Begin(transformMatrix: _scaleMatrix);
            CurrentScreen.Draw(gameTime, _spriteBatch);
            foreach (var worker in BackroundWorkers)
                worker.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void UpdateScale()
        {
            XScale = Device.PreferredBackBufferWidth / (float)IWindow.BaseScreenSize.X;
            YScale = Device.PreferredBackBufferHeight / (float)IWindow.BaseScreenSize.Y;
            _scaleMatrix = Matrix.CreateScale(XScale, YScale, 1.0f);
        }
    }
}