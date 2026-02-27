using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FormMatter.OpenGL.Input
{
	/// <summary>
	/// A class to control gamepad rumbling (if supported)
	/// </summary>
	public class GamepadRumbler
	{
		/// <summary>
		/// Index of what player to use
		/// </summary>
		public int PlayerIndex { get; set; }
		/// <summary>
		/// How long a rumble should last.
		/// </summary>
		public TimeSpan RumbleTime { get; set; }
		/// <summary>
		/// A value between 0 and 1, on how hard the motor should rumble.
		/// </summary>
		public float LeftMotor { get; set; } = 1;
		/// <summary>
		/// A value between 0 and 1, on how hard the motor should rumble.
		/// </summary>
		public float RightMotor { get; set; } = 1;

		private TimeSpan _lasted = TimeSpan.Zero;
		private bool _isRumbling = false;

		/// <summary>
		/// Start rumbling the controller
		/// </summary>
		public void Rumble()
		{
			_lasted = TimeSpan.Zero;
			_isRumbling = true;
			GamePad.SetVibration(PlayerIndex, LeftMotor, RightMotor);
		}

		/// <summary>
		/// Update the gamepad rumbler
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
		{
			if (_isRumbling)
			{
				_lasted += gameTime.ElapsedGameTime;
				if (_lasted > RumbleTime)
				{
					_isRumbling = false;
					GamePad.SetVibration(PlayerIndex, 0, 0);
				}
			}
		}
	}
}
