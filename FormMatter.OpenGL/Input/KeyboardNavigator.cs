using FormMatter.OpenGL.Controls;
using FormMatter.OpenGL.Views;
using Microsoft.Xna.Framework.Input;

namespace FormMatter.OpenGL.Input
{
	/// <summary>
	/// Event handler for input navigator events
	/// </summary>
	public delegate void KeyboardNavigatorOnInputEventHandler();

	/// <summary>
	/// A navigator that uses the keyboard to move
	/// </summary>
	public class KeyboardNavigator : BaseNavigator
	{
		/// <summary>
		/// Event that fires when ANY of the valid keys are entered
		/// </summary>
		public KeyboardNavigatorOnInputEventHandler? OnAnyKeyDown;

		/// <summary>
		/// Set of keys that allows the navigator to move left
		/// </summary>
		public List<Keys> LeftKeys { get; set; } = new List<Keys>();
		/// <summary>
		/// Event that fires when a left key is down
		/// </summary>
		public KeyboardNavigatorOnInputEventHandler? OnLeftKeyDown;
		/// <summary>
		/// Set of keys that allows the navigator to move right
		/// </summary>
		public List<Keys> RightKeys { get; set; } = new List<Keys>();
		/// <summary>
		/// Event that fires when a right key is down
		/// </summary>
		public KeyboardNavigatorOnInputEventHandler? OnRightKeyDown;
		/// <summary>
		/// Set of keys that allows the navigator to up
		/// </summary>
		public List<Keys> UpKeys { get; set; } = new List<Keys>();
		/// <summary>
		/// Event that fires when a up key is down
		/// </summary>
		public KeyboardNavigatorOnInputEventHandler? OnUpKeyDown;
		/// <summary>
		/// Set of keys that allows the navigator to move down
		/// </summary>
		public List<Keys> DownKeys { get; set; } = new List<Keys>();
		/// <summary>
		/// Event that fires when a down key is down
		/// </summary>
		public KeyboardNavigatorOnInputEventHandler? OnDownKeyDown;
		/// <summary>
		/// Set of keys that allows the navigator to move enter
		/// </summary>
		public List<Keys> EnterKeys { get; set; } = new List<Keys>();
		/// <summary>
		/// Event that fires when a enter key is down
		/// </summary>
		public KeyboardNavigatorOnInputEventHandler? OnEnterKeyDown;

		private readonly IView _view;
		private bool _keyDown = false;

		/// <summary>
		/// Constructor with single keys
		/// </summary>
		/// <param name="view"></param>
		/// <param name="selector"></param>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="up"></param>
		/// <param name="down"></param>
		/// <param name="enter"></param>
		/// <param name="layer"></param>
		public KeyboardNavigator(IView view, IControl selector, Keys left, Keys right, Keys up, Keys down, Keys enter, int layer)
		{
			_view = view;
			Selector = selector;
			Selector.IsVisible = false;
			LeftKeys = new List<Keys>() { left };
			RightKeys = new List<Keys>() { right };
			UpKeys = new List<Keys>() { up };
			DownKeys = new List<Keys>() { down };
			EnterKeys = new List<Keys>() { enter };
			Layers = new List<int>() { layer };
		}

		/// <summary>
		/// Constructor with multiple keys
		/// </summary>
		/// <param name="view"></param>
		/// <param name="selector"></param>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="up"></param>
		/// <param name="down"></param>
		/// <param name="enter"></param>
		/// <param name="layers"></param>
		public KeyboardNavigator(IView view, IControl selector, List<Keys> left, List<Keys> right, List<Keys> up, List<Keys> down, List<Keys> enter, List<int> layers)
		{
			_view = view;
			Selector = selector;
			Selector.IsVisible = false;
			LeftKeys = left;
			RightKeys = right;
			UpKeys = up;
			DownKeys = down;
			EnterKeys = enter;
			Layers = layers;
		}

		/// <summary>
		/// Update the navigator
		/// </summary>
		public void Update()
		{
			var keyboardState = Keyboard.GetState();
			if (LeftKeys.Any(x => keyboardState.IsKeyDown(x)))
			{
				if (!_keyDown)
				{
					OnAnyKeyDown?.Invoke();
					OnLeftKeyDown?.Invoke();
					Left(_view);
				}
				_keyDown = true;
			}
			else if (RightKeys.Any(x => keyboardState.IsKeyDown(x)))
			{
				if (!_keyDown)
				{
					OnAnyKeyDown?.Invoke();
					OnRightKeyDown?.Invoke();
					Right(_view);
				}
				_keyDown = true;
			}
			else if (UpKeys.Any(x => keyboardState.IsKeyDown(x)))
			{
				if (!_keyDown)
				{
					OnAnyKeyDown?.Invoke();
					OnUpKeyDown?.Invoke();
					Up(_view);
				}
				_keyDown = true;
			}
			else if (DownKeys.Any(x => keyboardState.IsKeyDown(x)))
			{
				if (!_keyDown)
				{
					OnAnyKeyDown?.Invoke();
					OnDownKeyDown?.Invoke();
					Down(_view);
				}
				_keyDown = true;
			}
			else if (EnterKeys.Any(x => keyboardState.IsKeyDown(x)))
			{
				if (!_keyDown)
				{
					OnAnyKeyDown?.Invoke();
					OnEnterKeyDown?.Invoke();
					if (Focused != null)
					{
						switch (Focused)
						{
							case ButtonControl b:
								b.DoClick();
								break;
							case TextInputControl b:
								b.Focus();
								break;
						}
					}
				}
				_keyDown = true;
			}
			else
				_keyDown = false;
		}
	}
}
