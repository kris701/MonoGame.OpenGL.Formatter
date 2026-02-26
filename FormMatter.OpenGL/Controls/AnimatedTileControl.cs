using Microsoft.Xna.Framework;
using FormMatter.OpenGL.Controls.Elements;
using FormMatter.OpenGL.Textures;

namespace FormMatter.OpenGL.Controls
{
	/// <summary>
	/// A version of the <seealso cref="TileControl"/> that has an animated tile control behind it.
	/// </summary>
	public class AnimatedTileControl : TileControl
	{
		/// <summary>
		/// The tileset to animate from
		/// </summary>
		public TextureSetDefinition TileSet
		{
			get => _animatedElement.TileSet;
			set
			{
				_animatedElement.TileSet = value;
				_animatedElement.Finished = false;
			}
		}

		/// <summary>
		/// The current fram that is being displayed
		/// </summary>
		public int Frame
		{
			get => _animatedElement.Frame;
			set => _animatedElement.Frame = value;
		}

		/// <summary>
		/// If the background should loop
		/// </summary>
		public bool AutoPlay
		{
			get => _animatedElement.AutoPlay;
			set => _animatedElement.AutoPlay = value;
		}

		internal AnimatedTileElement _animatedElement;

		/// <summary>
		/// Main constructor
		/// </summary>
		public AnimatedTileControl()
		{
			_animatedElement = new AnimatedTileElement(this);
		}

		/// <summary>
		/// Initialize this and the animated element
		/// </summary>
		public override void Initialize()
		{
			_animatedElement.Initialize();
			base.Initialize();
		}

		/// <summary>
		/// Update this and the animated element.
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			_animatedElement.Update(gameTime);
			base.Update(gameTime);
		}
	}
}