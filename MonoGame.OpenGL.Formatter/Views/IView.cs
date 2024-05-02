using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.OpenGL.Formatter.Controls;

namespace MonoGame.OpenGL.Formatter.Views
{
    /// <summary>
    /// A View can be seen as a virtual window inside of a <seealso cref="IWindow"/>.
    /// A <seealso cref="IView"/> will always take up the entire screen.
    /// </summary>
    public interface IView
    {
        public Guid ID { get; set; }
        public IWindow Parent { get; set; }

        public void ClearLayer(int layer);
        public void AddControl(int layer, IControl control);
        public void RemoveControl(int layer, IControl control);

        public void Initialize();
        public void Update(GameTime gameTime);
        public void OnUpdate(GameTime gameTime);
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public void SwitchView(IView screen);
    }
}
