using FormMatter.OpenGL.Controls.Elements;
using FormMatter.OpenGL.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FormMatter.OpenGL.Controls
{
	/// <summary>
	/// A <seealso cref="LabelControl"/> with a "clickable" element to it.
	/// </summary>
	public class ButtonControl : LabelControl, IFocusable
	{
		/// <summary>
		/// Event handler for clicks
		/// </summary>
		/// <param name="parent"></param>
		public delegate void ButtonControlHandler(ButtonControl parent);

		/// <summary>
		/// Event for click
		/// </summary>
		public event ButtonControlHandler? OnClicked;

		/// <summary>
		/// Event for the mouse entering this controls area
		/// </summary>
		public event ButtonControlHandler? OnEnter;

		/// <summary>
		/// Event for the mouse leaving this controls area
		/// </summary>
		public event ButtonControlHandler? OnLeave;

		/// <summary>
		/// The base window reference
		/// </summary>
		public IWindow Parent { get; set; }

		/// <summary>
		/// Click sound effect ID
		/// </summary>
		public Guid ClickSound
		{
			get => _clickSoundElement.SoundEffect;
			set => _clickSoundElement.SoundEffect = value;
		}

		/// <summary>
		/// Fill color when clicked
		/// </summary>
		public Texture2D FillClickedColor { get; set; } = BasicTextures.GetBasicRectange(Color.Gray);
		/// <summary>
		/// Fill color when disabled
		/// </summary>
		public Texture2D FillDisabledColor { get; set; } = BasicTextures.GetBasicRectange(Color.DarkGray);
		/// <summary>
		/// Is the button enabled or disabled?
		/// </summary>
		public bool IsEnabled { get; set; } = true;

		private readonly SoundEffectElement _clickSoundElement;
		private bool _holding = false;
		private bool _blocked = false;

		private bool _enterBlocked = false;
		private bool _leaveBlocked = false;

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="onClicked"></param>
		public ButtonControl(IWindow parent, ButtonControlHandler? onClicked = null, ButtonControlHandler? onHover = null, ButtonControlHandler? onLeave = null)
		{
			Parent = parent;
			OnClicked += onClicked;
			OnEnter += onHover;
			OnLeave += onLeave;
			_clickSoundElement = new SoundEffectElement(parent);
		}

		/// <summary>
		/// Do the click method
		/// </summary>
		public void DoClick()
		{
			if (!IsEnabled || !IsVisible)
				return;
			_holding = true;
			_clickSoundElement.Trigger();
			OnClicked?.Invoke(this);
		}

		/// <summary>
		/// Update the control
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			if (IsEnabled && Parent.IsActive && OnClicked != null)
			{
				var mouseState = Mouse.GetState();
				var translatedPos = InputHelper.GetRelativePosition(Parent.XScale, Parent.YScale);
				var isWithin = (translatedPos.X > X && translatedPos.X < X + Width &&
					translatedPos.Y > Y && translatedPos.Y < Y + Height);

				if (isWithin)
				{
					_leaveBlocked = false;
					if (!_enterBlocked)
					{
						OnEnter?.Invoke(this);
						_enterBlocked = true;
					}
				}
				else
				{
					_enterBlocked = false;
					if (!_leaveBlocked)
					{
						OnLeave?.Invoke(this);
						_leaveBlocked = true;
					}
				}

				if (!_blocked && isWithin && IsVisible)
				{
					if (!_holding && mouseState.LeftButton == ButtonState.Pressed)
						_holding = true;
					else if (_holding && mouseState.LeftButton == ButtonState.Released)
					{
						_clickSoundElement.Trigger();
						OnClicked?.Invoke(this);
						_holding = false;
					}
				}
				else
				{
					if (_holding && mouseState.LeftButton == ButtonState.Released)
						_holding = false;
					if (mouseState.LeftButton == ButtonState.Pressed)
						_blocked = true;
					else
						_blocked = false;
				}

				if (!IsVisible)
					_holding = false;
			}

			base.Update(gameTime);
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
			if (_holding)
				DrawTile(gameTime, spriteBatch, FillClickedColor);
			if (!IsEnabled)
				DrawTile(gameTime, spriteBatch, FillDisabledColor);
		}
	}
}