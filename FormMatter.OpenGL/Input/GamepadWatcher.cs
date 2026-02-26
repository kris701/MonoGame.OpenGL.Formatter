using Microsoft.Xna.Framework.Input;

namespace FormMatter.OpenGL.Input
{
	/// <summary>
	/// An input watcher that listens for a gamepad key press
	/// </summary>
	public class GamepadWatcher
	{
		/// <summary>
		/// What key to listen to
		/// </summary>
		public Buttons Button { get; set; }
		/// <summary>
		/// Index of what controller to use
		/// </summary>
		public int PlayerIndex { get; set; } = 0;

		private bool _isDown = false;
		private readonly Action? _pressAction;
		private readonly Action? _unpressAction;

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="button"></param>
		/// <param name="pressAction"></param>
		/// <param name="unpresAction"></param>
		public GamepadWatcher(Buttons button, Action? pressAction = null, Action? unpresAction = null)
		{
			Button = button;
			_pressAction = pressAction;
			_unpressAction = unpresAction;
		}

		/// <summary>
		/// Update this key listener.
		/// </summary>
		public void Update()
		{
			var state = GamePad.GetState(PlayerIndex);
			if (!_isDown && state.IsButtonDown(Button))
			{
				if (_pressAction != null)
					_pressAction.Invoke();
				_isDown = true;
			}
			else if (_isDown && state.IsButtonUp(Button))
			{
				if (_unpressAction != null)
					_unpressAction.Invoke();
				_isDown = false;
			}
		}
	}
}