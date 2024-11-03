namespace MonoGame.OpenGL.Formatter.Controls
{
    public class StackPanelControl : CollectionControl
    {
        public enum Orientations { Vertical, Horizontal }

        public Orientations Orientation { get; set; } = Orientations.Vertical;
        public int Gap { get; set; } = 0;

        public StackPanelControl(List<IControl> children)
        {
            Children = children;
        }

        public override void Initialize()
        {
            float offset = 0;
            foreach (var child in Children)
            {
                if (Orientation == Orientations.Vertical)
                {
                    child.Y += offset;
                    if (child.Width == 0)
                        child.Width = Width;
                    offset += child.Height + Gap;
                }
                else
                {
                    child.X += offset;
                    if (child.Height == 0)
                        child.Height = Height;
                    offset += child.Width + Gap;
                }
            }
            base.Initialize();
        }
    }
}