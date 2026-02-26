using FormMatter.OpenGL.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FormMatter.OpenGL.Controls
{
	/// <summary>
	/// A <seealso cref="TileControl"/> with a border around it and a single internal <seealso cref="IControl"/> child.
	/// </summary>
	public class BorderControl : TileControl
	{
		/// <summary>
		/// Color of the border.
		/// </summary>
		public Texture2D BorderBrush { get; set; } = BasicTextures.GetBasicRectange(Color.Black);
		/// <summary>
		/// Thickness of the border
		/// </summary>
		public float Thickness { get; set; } = 2;
		/// <summary>
		/// Child element of the control
		/// </summary>
		public IControl Child { get; set; }

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="child"></param>
		public BorderControl(IControl child)
		{
			Child = child;
		}

		/// <summary>
		/// Initialize the control
		/// </summary>
		public override void Initialize()
		{
			Child.Initialize();
			base.Initialize();
		}

		/// <summary>
		/// Update the control
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			Child.Update(gameTime);
			base.Update(gameTime);
		}

		/// <summary>
		/// Draw the control.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="spriteBatch"></param>
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (!IsVisible)
				return;

			Child.Draw(gameTime, spriteBatch);
			spriteBatch.DrawLine(BorderBrush, new Vector2(Child.X - Thickness / 2, Child.Y), new Vector2(Child.X + Child.Width + Thickness / 2, Child.Y), Thickness, Alpha);
			spriteBatch.DrawLine(BorderBrush, new Vector2(Child.X, Child.Y), new Vector2(Child.X, Child.Y + Child.Height), Thickness, Alpha);
			spriteBatch.DrawLine(BorderBrush, new Vector2(Child.X + Child.Width, Child.Y), new Vector2(Child.X + Child.Width, Child.Y + Child.Height), Thickness, Alpha);
			spriteBatch.DrawLine(BorderBrush, new Vector2(Child.X - Thickness / 2, Child.Y + Child.Height), new Vector2(Child.X + Child.Width + Thickness / 2, Child.Y + Child.Height), Thickness, Alpha);
		}
	}
}