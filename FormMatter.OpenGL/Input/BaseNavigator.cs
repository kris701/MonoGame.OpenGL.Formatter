using FormMatter.OpenGL.Controls;
using FormMatter.OpenGL.Views;
using ToolsSharp;

namespace FormMatter.OpenGL.Input
{
	/// <summary>
	/// Core directional navigator
	/// </summary>
	public abstract class BaseNavigator
	{
		/// <summary>
		/// What layers to navigate within
		/// </summary>
		public List<int> Layers { get; set; } = new List<int>() { 0 };

		/// <summary>
		/// The visual control for the navigator
		/// </summary>
		public IControl Selector { get; set; }
		/// <summary>
		/// The position of the navigator
		/// </summary>
		public NavigatorSelectorLocations SelectorLocation { get; set; } = NavigatorSelectorLocations.Left;
		/// <summary>
		/// The currently focused element
		/// </summary>
		public IControl? Focused { get; set; } = null;

		/// <summary>
		/// The maximum distance that you allow to jump to
		/// </summary>
		public int MaxDistance { get; set; } = int.MaxValue;

		/// <summary>
		/// The arc of where to look for controls at
		/// </summary>
		public double Arc { get; set; } = 80;

		private static double _rad2Deg = 57.2957795130823209;

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="selector"></param>
		/// <param name="layers"></param>
		public BaseNavigator(IControl selector, List<int> layers)
		{
			Selector = selector;
			Layers = layers;
		}

		/// <summary>
		/// Goto the closes valid control in a view
		/// </summary>
		/// <param name="view"></param>
		public void GotoClosest(IView view)
		{
			IControl? any = null;
			var currentX = Focused == null ? 0 : Focused.X;
			var currentY = Focused == null ? 0 : Focused.Y;
			int shortest = int.MaxValue;
			foreach (var layer in Layers)
			{
				var controls = view.GetAll(layer).Where(x => x is IFocusable).Cast<IFocusable>().Where(x => x.IsVisible && x.IsEnabled);
				foreach (var control in controls)
				{
					if (control == any || control == Focused || control == Selector)
						continue;

					int dist = (int)MathHelper.EuclideanDistance2D(new System.Drawing.Point((int)control.X, (int)control.Y), new System.Drawing.Point((int)currentX, (int)currentY));
					if (dist < shortest && dist < MaxDistance)
					{
						any = control;
						shortest = dist;
					}
				}
			}

			if (any != null)
			{
				Focused = any;
				UpdateFocusedPosition();
			}
		}

		internal bool Left(IView view)
		{
			IControl? any = null;
			var currentX = Focused == null ? 0 : Focused.X;
			var currentY = Focused == null ? 0 : Focused.Y;
			int shortest = int.MaxValue;
			foreach (var layer in Layers)
			{
				var controls = view.GetAll(layer).Where(x => x is IFocusable).Cast<IFocusable>().Where(x => x.IsVisible && x.IsEnabled);
				foreach (var control in controls)
				{
					if (control == any || control == Focused || control == Selector)
						continue;
					if (control.X >= currentX)
						continue;

					var angleRad = Math.Atan2(currentX - control.X, currentY - control.Y);
					var angle = angleRad * _rad2Deg;
					if (angle > 90 + (Arc / 2))
						continue;
					if (angle < 90 - (Arc / 2))
						continue;

					int dist = (int)MathHelper.EuclideanDistance2D(new System.Drawing.Point((int)control.X, (int)control.Y), new System.Drawing.Point((int)currentX, (int)currentY));
					if (dist < shortest && dist < MaxDistance)
					{
						any = control;
						shortest = dist;
					}
				}
			}

			if (any != null)
			{
				Focused = any;
				UpdateFocusedPosition();
				return true;
			}
			return false;
		}

		internal bool Right(IView view)
		{
			IControl? any = null;
			var currentX = Focused == null ? 0 : Focused.X;
			var currentY = Focused == null ? 0 : Focused.Y;
			int shortest = int.MaxValue;
			foreach (var layer in Layers)
			{
				var controls = view.GetAll(layer).Where(x => x is IFocusable).Cast<IFocusable>().Where(x => x.IsVisible && x.IsEnabled);
				foreach (var control in controls)
				{
					if (control == any || control == Focused || control == Selector)
						continue;
					if (control.X <= currentX)
						continue;

					var angleRad = Math.Atan2(control.X - currentX, currentY - control.Y);
					var angle = angleRad * _rad2Deg;
					if (angle > 90 + (Arc / 2))
						continue;
					if (angle < 90 - (Arc / 2))
						continue;

					int dist = (int)MathHelper.EuclideanDistance2D(new System.Drawing.Point((int)control.X, (int)control.Y), new System.Drawing.Point((int)currentX, (int)currentY));
					if (dist < shortest && dist < MaxDistance)
					{
						any = control;
						shortest = dist;
					}
				}
			}

			if (any != null)
			{
				Focused = any;
				UpdateFocusedPosition();
				return true;
			}
			return false;
		}

		internal bool Up(IView view)
		{
			IControl? any = null;
			var currentX = Focused == null ? 0 : Focused.X;
			var currentY = Focused == null ? 0 : Focused.Y;
			int shortest = int.MaxValue;
			foreach (var layer in Layers)
			{
				var controls = view.GetAll(layer).Where(x => x is IFocusable).Cast<IFocusable>().Where(x => x.IsVisible && x.IsEnabled);
				foreach (var control in controls)
				{
					if (control == any || control == Focused || control == Selector)
						continue;
					if (control.Y >= currentY)
						continue;

					var angleRad = Math.Atan2(currentX - control.X, currentY - control.Y);
					var angle = angleRad * _rad2Deg;
					if (angle > (Arc / 2))
						continue;
					if (angle < -(Arc / 2))
						continue;

					int dist = (int)MathHelper.EuclideanDistance2D(new System.Drawing.Point((int)control.X, (int)control.Y), new System.Drawing.Point((int)currentX, (int)currentY));
					if (dist < shortest && dist < MaxDistance)
					{
						any = control;
						shortest = dist;
					}
				}
			}

			if (any != null)
			{
				Focused = any;
				UpdateFocusedPosition();
				return true;
			}
			return false;
		}

		internal bool Down(IView view)
		{
			IControl? any = null;
			var currentX = Focused == null ? 0 : Focused.X;
			var currentY = Focused == null ? 0 : Focused.Y;
			int shortest = int.MaxValue;
			foreach (var layer in Layers)
			{
				var controls = view.GetAll(layer).Where(x => x is IFocusable).Cast<IFocusable>().Where(x => x.IsVisible && x.IsEnabled);
				foreach (var control in controls)
				{
					if (control == any || control == Focused || control == Selector)
						continue;
					if (control.Y <= currentY)
						continue;

					var angleRad = Math.Atan2(currentX - control.X, control.Y - currentY);
					var angle = angleRad * _rad2Deg;
					if (angle > (Arc / 2))
						continue;
					if (angle < -(Arc / 2))
						continue;

					int dist = (int)MathHelper.EuclideanDistance2D(new System.Drawing.Point((int)control.X, (int)control.Y), new System.Drawing.Point((int)currentX, (int)currentY));
					if (dist < shortest && dist < MaxDistance)
					{
						any = control;
						shortest = dist;
					}
				}
			}

			if (any != null)
			{
				Focused = any;
				UpdateFocusedPosition();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Update the focused element position
		/// </summary>
		public void UpdateFocusedPosition()
		{
			if (Focused == null)
				return;
			switch (SelectorLocation)
			{
				case NavigatorSelectorLocations.Left:
					Selector.X = Focused.X - Selector.Width;
					Selector.Y = Focused.Y + Focused.Height / 2 - Selector.Height / 2;
					break;
				case NavigatorSelectorLocations.Right:
					Selector.X = Focused.X + Focused.Width;
					Selector.Y = Focused.Y + Focused.Height / 2 - Selector.Height / 2;
					break;
				case NavigatorSelectorLocations.Top:
					Selector.X = Focused.X + Focused.Width / 2 - Selector.Width / 2;
					Selector.Y = Focused.Y - Selector.Height;
					break;
				case NavigatorSelectorLocations.Bottom:
					Selector.X = Focused.X + Focused.Width / 2 - Selector.Width / 2;
					Selector.Y = Focused.Y + Focused.Height;
					break;
			}
			Selector.IsVisible = true;
		}
	}
}
