using FormMatter.OpenGL.Controls.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormMatter.OpenGL.Controls
{
	/// <summary>
	/// A version of a <seealso cref="TileControl"/> with a text element in it.
	/// </summary>
	public class LabelControl : TileControl
	{
		/// <summary>
		/// The text to display
		/// </summary>
		public string Text
		{
			get => _element.Text;
			set => _element.Text = value;
		}

		/// <summary>
		/// The font to use
		/// </summary>
		public SpriteFont Font
		{
			get => _element.Font;
			set => _element.Font = value;
		}

		/// <summary>
		/// The color the font should be.
		/// </summary>
		public Color FontColor
		{
			get => _element.FontColor;
			set => _element.FontColor = value;
		}

		/// <summary>
		/// The X position of the text element
		/// </summary>
		public float TextX => _element._textX;
		/// <summary>
		/// The Y position of the text element
		/// </summary>
		public float TextY => _element._textY;
		/// <summary>
		/// The width of the text element
		/// </summary>
		public float TextWidth => _element._textWidth;
		/// <summary>
		/// The height of the text element
		/// </summary>
		public float TextHeight => _element._textHeight;

		private readonly TextElement _element;

		/// <summary>
		/// Main constructor
		/// </summary>
		public LabelControl()
		{
			_element = new TextElement(this)
			{
				Text = "",
				FontColor = Color.White
			};
		}

		/// <summary>
		/// Initialize the control
		/// </summary>
		public override void Initialize()
		{
			_element.Initialize();
			base.Initialize();
		}

		/// <summary>
		/// Update the control
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			_element.Update(gameTime);
		}

		/// <summary>
		/// Draw the control
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="spriteBatch"></param>
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (!IsVisible)
				return;

			base.Draw(gameTime, spriteBatch);
			_element.Draw(gameTime, spriteBatch);
		}
	}
}