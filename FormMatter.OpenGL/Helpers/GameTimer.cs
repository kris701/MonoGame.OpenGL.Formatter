using Microsoft.Xna.Framework;

namespace FormMatter.OpenGL.Helpers
{
	/// <summary>
	/// A simple internal game timer
	/// </summary>
	public class GameTimer
	{
		/// <summary>
		/// The target time to trigger in
		/// </summary>
		public TimeSpan Target { get; }
		private TimeSpan _last = TimeSpan.Zero;
		private readonly Action<TimeSpan> _func;

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="target"></param>
		/// <param name="func"></param>
		public GameTimer(TimeSpan target, Action<TimeSpan> func)
		{
			Target = target;
			_func = func;
		}

		/// <summary>
		/// Update the game timer
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
		{
			_last += gameTime.ElapsedGameTime;
			if (_last > Target)
			{
				_func.Invoke(_last);
				_last = TimeSpan.Zero;
			}
		}
	}
}