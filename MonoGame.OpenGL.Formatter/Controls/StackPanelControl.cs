namespace MonoGame.OpenGL.Formatter.Controls
{
    public class StackPanelControl : CollectionControl
    {
        public StackPanelControl(List<IControl> children)
        {
            Children = children;
        }

        public override void Initialize()
        {
            foreach (var child in Children)
            {
                child.Y += child.Height;
                if (child.Width == 0)
                    child.Width = Width;
            }
            base.Initialize();
        }
    }
}