using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormMatter.OpenGL.Controls
{
	/// <summary>
	/// Allignment options
	/// </summary>
	public enum HorizontalAlignment
	{ None, Left, Middle, Right }

	/// <summary>
	/// Allignment options
	/// </summary>
	public enum VerticalAlignment
	{ None, Top, Middle, Bottom }

	/// <summary>
	/// Basic control interface.
	/// </summary>
	public interface IControl
	{
		/// <summary>
		/// Direction to auto align in the horizontal plain
		/// </summary>
		public HorizontalAlignment HorizontalAlignment { get; set; }
		/// <summary>
		/// Direction to auto align in the vertical plain
		/// </summary>
		public VerticalAlignment VerticalAlignment { get; set; }

		/// <summary>
		/// Is the control visible or not?
		/// </summary>
		public bool IsVisible { get; set; }
		/// <summary>
		/// Check for the control being initialized
		/// </summary>
		public bool IsInitialized { get; set; }
		/// <summary>
		/// The X position of the control
		/// </summary>
		public float X { get; set; }
		/// <summary>
		/// The Y position of the control
		/// </summary>
		public float Y { get; set; }
		/// <summary>
		/// The width of the control
		/// </summary>
		public float Width { get; set; }
		/// <summary>
		/// The height of the control
		/// </summary>
		public float Height { get; set; }
		/// <summary>
		/// The opacity of the control, as a value between 0 and 256.
		/// </summary>
		public int Alpha { get; set; }
		/// <summary>
		/// Optional data tag
		/// </summary>
		public object? Tag { get; set; }
		/// <summary>
		/// Rotation of the control
		/// </summary>
		public float Rotation { get; set; }
		/// <summary>
		/// Viewport of the control
		/// </summary>
		public Rectangle ViewPort { get; set; }

		/// <summary>
		/// Initialization method of the control
		/// </summary>
		public void Initialize();

		/// <summary>
		/// Update method of the control
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime);

		/// <summary>
		/// Draw method of the control
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="spriteBatch"></param>
		public void Draw(GameTime gameTime, SpriteBatch spriteBatch);

		/// <summary>
		/// Offset this control from another one.
		/// </summary>
		/// <param name="parent"></param>
		public void OffsetFrom(IControl parent);

		/// <summary>
		/// Get all controls in this control, including itself
		/// </summary>
		/// <returns></returns>
		public List<IControl> GetAll();
	}
}