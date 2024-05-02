using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.OpenGL.Formatter.Controls.Elements
{
    public class TextElement
    {
        public IControl Parent { get; }
        private string _text = "";
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (value != _text)
                    _textChanged = true;
                _text = value;
            }
        }
        public Color FontColor { get; set; } = Color.White;
        private SpriteFont? _font = null;
        public SpriteFont Font
        {
            get
            {
                if (_font == null)
                    throw new Exception($"Font not set for text element with text '{Text}'");
                return _font;
            }
            set
            {
                _font = value;
                _textChanged = true;
            }
        }

        internal float _textX = 0;
        internal float _textY = 0;
        internal float _textWidth = 0;
        internal float _textHeight = 0;
        internal bool _textChanged = true;

        public TextElement(IControl parent)
        {
            Parent = parent;
        }

        private void UpdateTextPositions()
        {
            var size = Font.MeasureString(Text);
            _textWidth = size.X;
            _textHeight = size.Y;
            _textX = Parent.X + Parent.Width / 2 - _textWidth / 2;
            _textY = Parent.Y + Parent.Height / 2 - _textHeight / 2;
        }

        public void Initialize()
        {
            if (Text != "")
                UpdateTextPositions();
        }

        public void Update(GameTime gameTime)
        {
            if (_textChanged && Text != "")
            {
                _textChanged = false;
                UpdateTextPositions();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Text != "")
                spriteBatch.DrawString(
                    Font,
                    Text,
                    new Vector2(_textX + _textWidth / 2, _textY + _textHeight / 2),
                    new Color(FontColor.R, FontColor.G, FontColor.B, Parent.Alpha),
                    Parent.Rotation,
                    new Vector2(_textWidth / 2, _textHeight / 2),
                    1f,
                    SpriteEffects.None,
                    0);
        }
    }
}
