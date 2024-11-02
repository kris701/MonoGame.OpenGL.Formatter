using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.OpenGL.Formatter.Textures;

namespace MonoGame.OpenGL.Formatter.Controls.Elements
{
    public class AnimatedTileElement
    {
        public delegate void OnAnimationDoneHandler(TileControl parent);
        public OnAnimationDoneHandler? OnAnimationDone;

        public TileControl Parent { get; set; }
        private TimeSpan _frameTime;
        private List<Texture2D> _textures = new List<Texture2D>();
        private TextureSetDefinition _tileSet = new TextureSetDefinition(Guid.Empty, 1000, new List<string>(), false);
        public TextureSetDefinition TileSet
        {
            get => _tileSet;
            set {
                _tileSet = value;
                _textures = _tileSet.GetLoadedContent();
                _frameTime = TimeSpan.FromMilliseconds(_tileSet.FrameTime);
                Initialize();
            }
        }
        public int Frame { get; set; } = 0;
        public bool AutoPlay { get; set; } = true;
        public bool Finished { get; set; } = false;
        private TimeSpan _currentFrameTime = TimeSpan.Zero;

        public AnimatedTileElement(TileControl parent)
        {
            Parent = parent;
        }

        public void Initialize()
        {
            if (_textures.Count > 0)
            {
                Frame = 0;
                int targetW = _textures[0].Width;
                int targetH = _textures[0].Height;
                foreach (var tile in _textures.Skip(1))
                    if (tile.Width != targetW || tile.Height != targetH)
                        throw new Exception("Animated tileset must have the same size!");

                if (Parent.Width == 0)
                    Parent.Width = _textures[0].Width;
                if (Parent.Height == 0)
                    Parent.Height = _textures[0].Height;
                Frame = 0;
                Parent.FillColor = _textures[0];
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_textures.Count <= 1)
                return;

            if (Finished && !AutoPlay)
                return;
            else
            {
                _currentFrameTime += gameTime.ElapsedGameTime;
                if (_currentFrameTime >= _frameTime)
                {
                    Frame++;
                    if (Frame >= _textures.Count)
                    {
                        if (AutoPlay)
                            Frame = 0;
                        else
                        {
                            Finished = true;
                            Frame = _textures.Count - 1;
                        }
                        OnAnimationDone?.Invoke(Parent);
                    }
                    Parent.FillColor = _textures[Frame];
                    _currentFrameTime = TimeSpan.Zero;
                }
            }
        }
    }
}
