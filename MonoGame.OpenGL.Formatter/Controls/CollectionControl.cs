using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.OpenGL.Formatter.Controls
{
	/// <summary>
	/// Base control for handling a set of children inside of it.
	/// </summary>
	public abstract class CollectionControl : BaseControl
	{
		/// <summary>
		/// The set of children in this control
		/// </summary>
		public List<IControl> Children { get; set; } = new List<IControl>();

		private bool _visibilityChanged = true;

		/// <summary>
		/// Initialize the position and visibility of each child control within this control
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();
			foreach (var child in Children)
			{
				child.OffsetFrom(this);
				child.Initialize();
			}
			UpdateVisibility();
		}

		/// <summary>
		/// Change the visibility of all child controls to that of this one
		/// </summary>
		public void UpdateVisibility()
		{
			if (IsVisible != _visibilityChanged)
			{
				_visibilityChanged = IsVisible;
				foreach (var child in Children)
					child.IsVisible = IsVisible;
			}
		}

		/// <summary>
		/// Update this control
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			UpdateVisibility();
			foreach (var child in Children)
				child.Update(gameTime);
		}

		/// <summary>
		/// Draw all the child controls
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="spriteBatch"></param>
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (!IsVisible)
				return;

			foreach (var child in Children)
				child.Draw(gameTime, spriteBatch);
		}
	}
}