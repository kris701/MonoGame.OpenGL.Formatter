﻿using Microsoft.Xna.Framework.Input;

namespace MonoGame.OpenGL.Formatter.Input
{
	/// <summary>
	/// An input watcher that listens for a set of keys
	/// </summary>
	public class KeysWatcher
	{
		/// <summary>
		/// List of keys to listen to
		/// </summary>
		public List<Keys> Keys { get; set; }

		private bool _isDown = false;
		private readonly Action? _pressAction;
		private readonly Action? _unpressAction;

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="keys"></param>
		/// <param name="pressAction"></param>
		/// <param name="unpresAction"></param>
		public KeysWatcher(List<Keys> keys, Action? pressAction = null, Action? unpresAction = null)
		{
			Keys = keys;
			_pressAction = pressAction;
			_unpressAction = unpresAction;
		}

		/// <summary>
		/// Update the key watchers.
		/// </summary>
		/// <param name="state"></param>
		public void Update(KeyboardState state)
		{
			if (!_isDown && Keys.All(state.IsKeyDown))
			{
				if (_pressAction != null)
					_pressAction.Invoke();
				_isDown = true;
			}
			else if (_isDown && Keys.Any(state.IsKeyUp))
			{
				if (_unpressAction != null)
					_unpressAction.Invoke();
				_isDown = false;
			}
		}
	}
}