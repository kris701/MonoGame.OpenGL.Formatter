using FormMatter.OpenGL.Controls;
using FormMatter.OpenGL.Views;
using Microsoft.Xna.Framework.Input;

namespace FormMatter.OpenGL.Input
{
	/// <summary>
	/// Event handler for input navigator events
	/// </summary>
	public delegate void GamepadNavigatorOnInputEventHandler();

	/// <summary>
	/// A navigator that uses the gamepad to move
	/// </summary>
	public class GamepadNavigator : BaseNavigator
	{
		/// <summary>
		/// Event that fires when ANY of the valid keys are entered
		/// </summary>
		public GamepadNavigatorOnInputEventHandler? OnAnyKeyDown;

		/// <summary>
		/// Set of keys that allows the navigator to move left
		/// </summary>
		public List<Buttons> LeftKeys { get; set; } = new List<Buttons>();
		/// <summary>
		/// Event that fires when a left key is down
		/// </summary>
		public GamepadNavigatorOnInputEventHandler? OnLeftKeyDown;
		/// <summary>
		/// Set of keys that allows the navigator to move right
		/// </summary>
		public List<Buttons> RightKeys { get; set; } = new List<Buttons>();
		/// <summary>
		/// Event that fires when a right key is down
		/// </summary>
		public GamepadNavigatorOnInputEventHandler? OnRightKeyDown;
		/// <summary>
		/// Set of keys that allows the navigator to up
		/// </summary>
		public List<Buttons> UpKeys { get; set; } = new List<Buttons>();
		/// <summary>
		/// Event that fires when a up key is down
		/// </summary>
		public GamepadNavigatorOnInputEventHandler? OnUpKeyDown;
		/// <summary>
		/// Set of keys that allows the navigator to move down
		/// </summary>
		public List<Buttons> DownKeys { get; set; } = new List<Buttons>();
		/// <summary>
		/// Event that fires when a down key is down
		/// </summary>
		public GamepadNavigatorOnInputEventHandler? OnDownKeyDown;
		/// <summary>
		/// Set of keys that allows the navigator to move enter
		/// </summary>
		public List<Buttons> EnterKeys { get; set; } = new List<Buttons>();
		/// <summary>
		/// Event that fires when a enter key is down
		/// </summary>
		public GamepadNavigatorOnInputEventHandler? OnEnterKeyDown;

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
		public GamepadNavigator(IView view, IControl selector, Buttons left, Buttons right, Buttons up, Buttons down, Buttons enter, int layer)
		{
			_view = view;
			Selector = selector;
			Selector.IsVisible = false;
			LeftKeys = new List<Buttons>() { left };
			RightKeys = new List<Buttons>() { right };
			UpKeys = new List<Buttons>() { up };
			DownKeys = new List<Buttons>() { down };
			EnterKeys = new List<Buttons>() { enter };
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
		public GamepadNavigator(IView view, IControl selector, List<Buttons> left, List<Buttons> right, List<Buttons> up, List<Buttons> down, List<Buttons> enter, List<int> layers)
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
			var gamepadState = GamePad.GetState(0);
			if (LeftKeys.Any(x => gamepadState.IsButtonDown(x)))
			{
				if (!_keyDown)
				{
					OnAnyKeyDown?.Invoke();
					OnLeftKeyDown?.Invoke();
					Left(_view);
				}
				_keyDown = true;
			}
			else if (RightKeys.Any(x => gamepadState.IsButtonDown(x)))
			{
				if (!_keyDown)
				{
					OnAnyKeyDown?.Invoke();
					OnRightKeyDown?.Invoke();
					Right(_view);
				}
				_keyDown = true;
			}
			else if (UpKeys.Any(x => gamepadState.IsButtonDown(x)))
			{
				if (!_keyDown)
				{
					OnAnyKeyDown?.Invoke();
					OnUpKeyDown?.Invoke();
					Up(_view);
				}
				_keyDown = true;
			}
			else if (DownKeys.Any(x => gamepadState.IsButtonDown(x)))
			{
				if (!_keyDown)
				{
					OnAnyKeyDown?.Invoke();
					OnDownKeyDown?.Invoke();
					Down(_view);
				}
				_keyDown = true;
			}
			else if (EnterKeys.Any(x => gamepadState.IsButtonDown(x)))
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
