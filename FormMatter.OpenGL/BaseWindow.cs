using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FormMatter.OpenGL.Audio;
using FormMatter.OpenGL.BackgroundWorkers;
using FormMatter.OpenGL.Fonts;
using FormMatter.OpenGL.Textures;
using FormMatter.OpenGL.Views;
using System.Reflection;

namespace FormMatter.OpenGL
{
	/// <summary>
	/// A base implementation of the <seealso cref="IWindow"/> interface and <seealso cref="Game"/> class.
	/// </summary>
	public class BaseWindow : Game, IWindow
	{
		/// <summary>
		/// The scale, compared to <seealso cref="IWindow.BaseScreenSize"/>, on the x axis
		/// </summary>
		public float XScale { get; private set; } = 1;
		/// <summary>
		/// The scale, compared to <seealso cref="IWindow.BaseScreenSize"/>, on the y axis
		/// </summary>
		public float YScale { get; private set; } = 1;

		/// <summary>
		/// The current instance of an <seealso cref="IView"/> that is displayed.
		/// </summary>
		public IView CurrentScreen { get; set; }
		/// <summary>
		/// List of background workers.
		/// </summary>
		public List<IBackgroundWorker> BackroundWorkers { get; set; } = new List<IBackgroundWorker>();
		/// <summary>
		/// Main audio controller to load and play audio.
		/// </summary>
		public AudioController Audio { get; private set; }
		/// <summary>
		/// Main texture controller to load and use textures.
		/// </summary>
		public TextureController Textures { get; private set; }
		/// <summary>
		/// Main font controller to load and use textures.
		/// </summary>
		public FontController Fonts { get; private set; }
		/// <summary>
		/// Boolean describing if the current window is active or not.
		/// </summary>
		public new bool IsActive { get => base.IsActive; }

		/// <summary>
		/// OpenGL graphics device
		/// </summary>
		public GraphicsDeviceManager Device { get; }

		private Matrix _scaleMatrix;
		private SpriteBatch? _spriteBatch;
		private readonly string? _title;

		/// <summary>
		/// Main constructor
		/// </summary>
		public BaseWindow() : base()
		{
			Device = new GraphicsDeviceManager(this);
			UpdateScale();
		}

		/// <summary>
		/// Constructor where you can set a title
		/// </summary>
		/// <param name="title"></param>
		public BaseWindow(string title) : this()
		{
			_title = title;
		}

		/// <summary>
		/// Initialize the window and the current view
		/// </summary>
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

		/// <summary>
		/// Load content
		/// </summary>
		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		/// <summary>
		/// Update the window and the current view
		/// </summary>
		/// <param name="gameTime"></param>
		protected override void Update(GameTime gameTime)
		{
			CurrentScreen.Update(gameTime);
			foreach (var worker in BackroundWorkers)
				worker.Update(gameTime);
			base.Update(gameTime);
		}

		/// <summary>
		/// Draw the window and the current view
		/// </summary>
		/// <param name="gameTime"></param>
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

		/// <summary>
		/// Update the scales
		/// </summary>
		public void UpdateScale()
		{
			XScale = Device.PreferredBackBufferWidth / (float)IWindow.BaseScreenSize.X;
			YScale = Device.PreferredBackBufferHeight / (float)IWindow.BaseScreenSize.Y;
			_scaleMatrix = Matrix.CreateScale(XScale, YScale, 1.0f);
		}
	}
}