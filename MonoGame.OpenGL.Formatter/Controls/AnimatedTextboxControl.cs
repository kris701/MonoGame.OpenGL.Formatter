using Microsoft.Xna.Framework;
using MonoGame.OpenGL.Formatter.Controls.Elements;
using MonoGame.OpenGL.Formatter.Textures;

namespace MonoGame.OpenGL.Formatter.Controls
{
    public class AnimatedTextboxControl : TextboxControl
    {
        public TextureSetDefinition TileSet
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

        public AnimatedTileElement AnimatedElement;

        public AnimatedTextboxControl()
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