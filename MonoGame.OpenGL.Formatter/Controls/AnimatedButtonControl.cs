using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.OpenGL.Formatter.Controls.Elements;

namespace MonoGame.OpenGL.Formatter.Controls
{
    public class AnimatedButtonControl : ButtonControl
    {
        public List<Texture2D> TileSet
        {
            get => AnimatedElement.TileSet;
            set
            {
                AnimatedElement.TileSet = value;
                AnimatedElement.Finished = false;
            }
        }
        public int Frame
        {
            get => AnimatedElement.Frame;
            set => AnimatedElement.Frame = value;
        }
        public bool AutoPlay
        {
            get => AnimatedElement.AutoPlay;
            set => AnimatedElement.AutoPlay = value;
        }
        public TimeSpan FrameTime
        {
            get => AnimatedElement.FrameTime;
            set => AnimatedElement.FrameTime = value;
        }
        public AnimatedTileElement AnimatedElement;

        public AnimatedButtonControl(IWindow parent, SpriteFont font, ClickedHandler? clicked = null) : base(parent, font, clicked)
        {
            AnimatedElement = new AnimatedTileElement(this);
        }

        public override void Initialize()
        {
            AnimatedElement.Initialize();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            AnimatedElement.Update(gameTime);
            base.Update(gameTime);
        }
    }
}
