using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormMatter.OpenGL.Controls
{
	/// <summary>
	/// A version of <seealso cref="TileControl"/> with a static text field inside of it.
	/// It is automatically wordwrapped.
	/// </summary>
	public class TextboxControl : TileControl
	{
		/// <summary>
		/// Word wrap types
		/// </summary>
		public enum WordWrapTypes { None, Word, Character }

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
		/// What type of wordwrapping to use
		/// </summary>
		public WordWrapTypes WordWrap { get; set; } = WordWrapTypes.None;

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
			switch (WordWrap)
			{
				case WordWrapTypes.None:
					ProcessNoWordWrap(str);
					break;
				case WordWrapTypes.Character:
					ProcessCharacterWordWrap(str);
					break;
				case WordWrapTypes.Word:
					ProcessWordWordWrap(str);
					break;
			}
		}

		private void ProcessNoWordWrap(string str)
		{
			var currentString = "";
			foreach (var character in str)
			{
				var size = Font.MeasureString(currentString + character);
				if (size.X > Width - Margin * 2)
				{
					var newLabel = CreateSubLabel(currentString);
					if (newLabel.Y + newLabel.Height > Y + Height - Margin)
						break;
					lines.Add(newLabel);
					break; // just trim the rest of the string
				}
				currentString += character;
			}
		}

		private void ProcessCharacterWordWrap(string str)
		{
			var currentString = "";
			foreach (var character in str)
			{
				var size = Font.MeasureString(currentString + character);
				if (size.X > Width - Margin * 2)
				{
					var newLabel = CreateSubLabel(currentString);
					if (newLabel.Y + newLabel.Height > Y + Height - Margin)
						break;
					lines.Add(newLabel);
					currentString = "";
				}
				currentString += character;
			}
			if (currentString != "")
			{
				var newLabel = CreateSubLabel(currentString);
				if (newLabel.Y + newLabel.Height > Y + Height - Margin)
					return;
				lines.Add(newLabel);
			}
		}

		private void ProcessWordWordWrap(string str)
		{
			var currentString = "";
			var wordSplit = str.Split(' ');
			foreach (var word in wordSplit)
			{
				var size = Font.MeasureString(currentString + " " + word);
				if (size.X > Width - Margin * 2)
				{
					var newLabel = CreateSubLabel(currentString);
					if (newLabel.Y + newLabel.Height > Y + Height - Margin)
						break;
					lines.Add(newLabel);
					currentString = "";
				}
				currentString += " " + word;
			}
			if (currentString != "")
			{
				var newLabel = CreateSubLabel(currentString);
				if (newLabel.Y + newLabel.Height > Y + Height - Margin)
					return;
				lines.Add(newLabel);
			}
		}

		private LabelControl CreateSubLabel(string str)
		{
			var size = Font.MeasureString(str);
			var newLabel = new LabelControl();
			newLabel.Font = Font;
			newLabel.Text = str;
			newLabel.FontColor = FontColor;
			newLabel.X = X;
			newLabel.Y = Y + size.Y * lines.Count + Margin;
			newLabel.Width = Width;
			newLabel.Height = size.Y;
			return newLabel;
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