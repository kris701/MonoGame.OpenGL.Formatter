using Microsoft.Xna.Framework.Input;

namespace MonoGame.OpenGL.Formatter.Input
{
	/// <summary>
	/// An input watcher that listens for a key press
	/// </summary>
	public class KeyWatcher
	{
		/// <summary>
		/// What key to listen to
		/// </summary>
		public Keys Key { get; set; }

		private bool _isDown = false;
		private readonly Action? _pressAction;
		private readonly Action? _unpressAction;

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="key"></param>
		/// <param name="pressAction"></param>
		/// <param name="unpresAction"></param>
		public KeyWatcher(Keys key, Action? pressAction = null, Action? unpresAction = null)
		{
			Key = key;
			_pressAction = pressAction;
			_unpressAction = unpresAction;
		}

		/// <summary>
		/// Update this key listener.
		/// </summary>
		/// <param name="state"></param>
		public void Update(KeyboardState state)
		{
			if (!_isDown && state.IsKeyDown(Key))
			{
				if (_pressAction != null)
					_pressAction.Invoke();
				_isDown = true;
			}
			else if (_isDown && state.IsKeyUp(Key))
			{
				if (_unpressAction != null)
					_unpressAction.Invoke();
				_isDown = false;
			}
		}
	}
}