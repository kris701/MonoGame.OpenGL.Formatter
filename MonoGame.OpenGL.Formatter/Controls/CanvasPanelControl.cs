namespace MonoGame.OpenGL.Formatter.Controls
{
	/// <summary>
	/// A direct implementation of a <seealso cref="CollectionControl"/>.
	/// </summary>
	public class CanvasPanelControl : CollectionControl
	{
		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="children"></param>
		public CanvasPanelControl(List<IControl> children)
		{
			Children = children;
		}
	}
}
