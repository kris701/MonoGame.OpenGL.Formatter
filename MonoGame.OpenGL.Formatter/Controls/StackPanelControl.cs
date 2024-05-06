using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.OpenGL.Formatter.Controls
{
    public class StackPanelControl : CollectionControl
    {
        public StackPanelControl(List<IControl> children)
        {
            Children = children;
        }
    }
}
