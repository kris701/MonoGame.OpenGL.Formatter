using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.OpenGL.Formatter.Audio;
using MonoGame.OpenGL.Formatter.BackgroundWorkers;
using MonoGame.OpenGL.Formatter.Fonts;
using MonoGame.OpenGL.Formatter.Textures;
using MonoGame.OpenGL.Formatter.Views;

namespace MonoGame.OpenGL.Formatter
{
    /// <summary>
    /// Basic interface your window must have to work with Formatter.
    /// </summary>
    public interface IWindow
    {
        public static readonly Point BaseScreenSize = new Point(1920, 1080);
        public float XScale { get; }
        public float YScale { get; }

        public IView CurrentScreen { get; set; }
        public List<IBackgroundWorker> BackroundWorkers { get; set; }
        public AudioController Audio { get; }
        public TextureController Textures { get; }
        public FontController Fonts { get; }
        public bool IsActive { get; }

        public GraphicsDeviceManager Device { get; }
        public ContentManager Content { get; }

        public void UpdateScale();
    }
}