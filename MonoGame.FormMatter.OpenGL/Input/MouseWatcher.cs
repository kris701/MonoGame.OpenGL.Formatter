using Microsoft.Xna.Framework.Input;

namespace MonoGame.FormMatter.OpenGL.Input
{
	/// <summary>
	/// An input watcher that listens for mouse positions
	/// </summary>
	public class MouseWatcher
	{
		private bool _clicked = false;

		private readonly Action? _action;

		/// <summary>
		/// Main constructor
		/// </summary>
		public MouseWatcher(Action? action = null)
		{
			_action = action;
		}

		/// <summary>
		/// Update the watcher
		/// </summary>
		public void Update()
		{
			var state = Mouse.GetState();
			if (state.LeftButton == ButtonState.Pressed)
			{
				if (!_clicked)
					_action?.Invoke();
				_clicked = true;
			}
			else
				_clicked = false;
		}
	}
}