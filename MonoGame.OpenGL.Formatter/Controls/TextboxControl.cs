﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.OpenGL.Formatter.Controls
{
	/// <summary>
	/// A version of <seealso cref="TileControl"/> with a static text field inside of it.
	/// It is automatically wordwrapped.
	/// </summary>
	public class TextboxControl : TileControl
	{
		private string _text = "";

		/// <summary>
		/// Text of the item
		/// </summary>
		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
				_textChanged = true;
			}
		}

		/// <summary>
		/// Color of the font
		/// </summary>
		public Color FontColor { get; set; } = Color.Black;
		private SpriteFont? _font;

		/// <summary>
		/// Font to use
		/// </summary>
		public SpriteFont Font
		{
			get
			{
				if (_font == null)
					throw new Exception($"Font not set for textbox with text '{Text}'");
				return _font;
			}
			set
			{
				_font = value;
				_textChanged = true;
			}
		}

		/// <summary>
		/// Margin between each line of text.
		/// </summary>
		public float Margin { get; set; } = 5;

		internal bool _textChanged = true;
		internal List<LabelControl> lines = new List<LabelControl>();

		/// <summary>
		/// Update the control
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			if (_textChanged && Text != "")
			{
				lines = new List<LabelControl>();
				var split = Text.Split(Environment.NewLine).ToList();
				split.RemoveAll(x => x == "");
				foreach (var line in split)
					ProcessString(line);
				_textChanged = false;
			}
			foreach (var line in lines)
				line.Update(gameTime);
			base.Update(gameTime);
		}

		private void ProcessString(string str)
		{
			var currentString = "";
			foreach (var character in str)
			{
				currentString += character;
				var size = Font.MeasureString(currentString);
				if (size.X > Width - Margin * 2)
				{
					var newLabel = new LabelControl();
					newLabel.Font = Font;
					newLabel.Text = currentString;
					newLabel.FontColor = FontColor;
					newLabel.X = X;
					newLabel.Y = Y + size.Y * lines.Count + Margin;
					newLabel.Width = Width;
					newLabel.Height = size.Y;
					if (newLabel.Y + newLabel.Height > Y + Height - Margin)
						break;
					lines.Add(newLabel);
					currentString = "";
				}
			}
			if (currentString != "")
			{
				var size = Font.MeasureString(currentString);
				var newLabel = new LabelControl();
				newLabel.Font = Font;
				newLabel.Text = currentString;
				newLabel.FontColor = FontColor;
				newLabel.X = X;
				newLabel.Y = Y + size.Y * lines.Count + Margin;
				newLabel.Width = Width;
				newLabel.Height = size.Y;
				if (newLabel.Y + newLabel.Height > Y + Height - Margin)
					return;
				lines.Add(newLabel);
			}
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
			foreach (var line in lines)
				line.Draw(gameTime, spriteBatch);
		}
	}
}