using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.FormMatter.OpenGL.Controls;

namespace MonoGame.FormMatter.OpenGL.Views
{
	/// <summary>
	/// A View can be seen as a virtual window inside of a <seealso cref="IWindow"/>.
	/// A <seealso cref="IView"/> will always take up the entire screen.
	/// </summary>
	public interface IView
	{
		/// <summary>
		/// Unique ID of the view
		/// </summary>
		public Guid ID { get; set; }
		/// <summary>
		/// Parent window reference
		/// </summary>
		public IWindow Parent { get; set; }

		/// <summary>
		/// Clear a layer of controls
		/// </summary>
		/// <param name="layer"></param>
		public void ClearLayer(int layer);

		/// <summary>
		/// Add a control to some layer
		/// </summary>
		/// <param name="layer"></param>
		/// <param name="control"></param>
		public void AddControl(int layer, IControl control);

		/// <summary>
		/// Remove a control from some layer
		/// </summary>
		/// <param name="layer"></param>
		/// <param name="control"></param>
		public void RemoveControl(int layer, IControl control);

		/// <summary>
		/// Initialize this view and all its controls
		/// </summary>
		public void Initialize();

		/// <summary>
		/// Update this view and all its controls
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime);

		/// <summary>
		/// Additional event that fires on update.
		/// </summary>
		/// <param name="gameTime"></param>
		public void OnUpdate(GameTime gameTime);

		/// <summary>
		/// Draw this view and all its controls
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="spriteBatch"></param>
		public void Draw(GameTime gameTime, SpriteBatch spriteBatch);

		/// <summary>
		/// Switch from this <seealso cref="IView"/> to another <seealso cref="IView"/>
		/// </summary>
		/// <param name="screen"></param>
		public void SwitchView(IView screen);
	}
}