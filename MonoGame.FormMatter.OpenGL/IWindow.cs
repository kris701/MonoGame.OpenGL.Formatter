using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.FormMatter.OpenGL.Audio;
using MonoGame.FormMatter.OpenGL.BackgroundWorkers;
using MonoGame.FormMatter.OpenGL.Fonts;
using MonoGame.FormMatter.OpenGL.Textures;
using MonoGame.FormMatter.OpenGL.Views;

namespace MonoGame.FormMatter.OpenGL
{
	/// <summary>
	/// Basic interface your window must have to work with Formatter.
	/// </summary>
	public interface IWindow
	{
		/// <summary>
		/// The basic screen size to use
		/// </summary>
		public static readonly Point BaseScreenSize = new Point(1920, 1080);
		/// <summary>
		/// The scale, compared to <seealso cref="BaseScreenSize"/>, on the x axis
		/// </summary>
		public float XScale { get; }
		/// <summary>
		/// The scale, compared to <seealso cref="BaseScreenSize"/>, on the y axis
		/// </summary>
		public float YScale { get; }

		/// <summary>
		/// The current instance of an <seealso cref="IView"/> that is displayed.
		/// </summary>
		public IView CurrentScreen { get; set; }
		/// <summary>
		/// List of background workers.
		/// </summary>
		public List<IBackgroundWorker> BackroundWorkers { get; set; }
		/// <summary>
		/// Main audio controller to load and play audio.
		/// </summary>
		public AudioController Audio { get; }
		/// <summary>
		/// Main texture controller to load and use textures.
		/// </summary>
		public TextureController Textures { get; }
		/// <summary>
		/// Main font controller to load and use textures.
		/// </summary>
		public FontController Fonts { get; }
		/// <summary>
		/// Boolean describing if the current window is active or not.
		/// </summary>
		public bool IsActive { get; }

		/// <summary>
		/// OpenGL graphics device
		/// </summary>
		public GraphicsDeviceManager Device { get; }
		/// <summary>
		/// Monogame content manager.
		/// </summary>
		public ContentManager Content { get; }

		/// <summary>
		/// Method to update the scales, based on the resolution set on the <seealso cref="Device"/>.
		/// </summary>
		public void UpdateScale();
	}
}