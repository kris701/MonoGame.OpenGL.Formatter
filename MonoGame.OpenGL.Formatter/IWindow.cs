using MonoGame.OpenGL.Formatter.Audio;
using MonoGame.OpenGL.Formatter.BackgroundWorkers;
using MonoGame.OpenGL.Formatter.Textures;
using MonoGame.OpenGL.Formatter.Views;
using Microsoft.Xna.Framework;

namespace MonoGame.OpenGL.Formatter
{
    public interface IWindow
    {
        public static readonly Point BaseScreenSize = new Point(1920, 1080);
        public float XScale { get; }
        public float YScale { get; }

        public IView CurrentScreen { get; set; }
        public List<IBackgroundWorker> BackroundWorkers { get; set; }
        public AudioController AudioController { get; }
        public TextureController TextureController { get; }
        public bool IsActive { get; }
    }
}
