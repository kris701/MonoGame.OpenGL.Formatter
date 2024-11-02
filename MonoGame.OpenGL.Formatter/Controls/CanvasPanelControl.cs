using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.OpenGL.Formatter.Controls
{
    public class CanvasPanelControl : CollectionControl
    {
        public CanvasPanelControl(List<IControl> children)
        {
            Children = children;
        }
    }
}
