﻿namespace MonoGame.OpenGL.Formatter.Controls
{
	/// <summary>
	/// A version of the <seealso cref="CollectionControl"/> where the elements inside it are automatically stacked.
	/// </summary>
	public class StackPanelControl : CollectionControl
	{
		/// <summary>
		/// Orientation options
		/// </summary>
		public enum Orientations { Vertical, Horizontal }

		/// <summary>
		/// Orientation of the stacking
		/// </summary>
		public Orientations Orientation { get; set; } = Orientations.Vertical;
		/// <summary>
		/// Additional gap between elements
		/// </summary>
		public int Gap { get; set; } = 0;

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="children"></param>
		public StackPanelControl(List<IControl> children)
		{
			Children = children;
		}

		/// <summary>
		/// Initialize the control
		/// </summary>
		public override void Initialize()
		{
			float offset = 0;
			foreach (var child in Children)
			{
				if (Orientation == Orientations.Vertical)
				{
					child.Y += offset;
					if (child.Width == 0)
						child.Width = Width;
					offset += child.Height + Gap;
				}
				else
				{
					child.X += offset;
					if (child.Height == 0)
						child.Height = Height;
					offset += child.Width + Gap;
				}
			}
			base.Initialize();
		}
	}
}