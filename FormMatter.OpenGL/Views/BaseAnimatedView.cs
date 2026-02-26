using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FormMatter.OpenGL.Controls;
using FormMatter.OpenGL.Textures;

namespace FormMatter.OpenGL.Views
{
	/// <summary>
	/// A base class for a texture animated <seealso cref="IView"/>
	/// </summary>
	public abstract class BaseAnimatedView : BaseView
	{
		public enum FadeState
		{ AnimateIn, Hold, AnimateOut, PostHold }

		public FadeState State { get; set; } = FadeState.AnimateIn;

		private readonly AnimatedTileControl _tile;
		private readonly TextureSetDefinition _in;
		private readonly TextureSetDefinition _out;

		private IView? _switchTo;

		public BaseAnimatedView(IWindow parent, Guid id, TextureSetDefinition inSet, TextureSetDefinition outSet) : base(parent, id)
		{
			_in = inSet;
			_out = outSet;
			_tile = new AnimatedTileControl()
			{
				Width = IWindow.BaseScreenSize.X,
				Height = IWindow.BaseScreenSize.Y,
				TileSet = _in,
				AutoPlay = false
			};
			_tile._animatedElement.OnAnimationDone += (s) =>
			{
				switch (State)
				{
					case FadeState.AnimateIn:
						State = FadeState.Hold;
						_tile.IsVisible = false;
						break;

					case FadeState.AnimateOut:
						State = FadeState.PostHold;
						break;
				}
			};
			_tile.Initialize();
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);
			if (_tile.IsVisible)
				_tile.Draw(gameTime, spriteBatch);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (_tile.IsVisible)
				_tile.Update(gameTime);
			if (State == FadeState.PostHold)
				if (_switchTo != null)
					Parent.CurrentScreen = _switchTo;
		}

		public override void SwitchView(IView screen)
		{
			if (_switchTo == null)
			{
				_tile.TileSet = _out;
				_tile.Frame = 0;
				_tile.IsVisible = true;
				_tile.Initialize();
				_switchTo = screen;
				State = FadeState.AnimateOut;
			}
		}
	}
}