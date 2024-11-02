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
            float offset = 0;
            foreach (var child in Children)
            {
                child.Y += offset;
                if (child.Width == 0)
                    child.Width = Width;
                offset += child.Height;
            }
            base.Initialize();
        }
    }
}