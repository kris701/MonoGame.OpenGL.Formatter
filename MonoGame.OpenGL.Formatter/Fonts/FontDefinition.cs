using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.OpenGL.Formatter.Fonts
{
    public class FontDefinition : LoadableContent<SpriteFont>
    {
        public Guid ID { get; set; }
        public string Content { get; set; }

        public FontDefinition(Guid iD, string content, bool isDefered) : base(isDefered)
        {
            ID = iD;
            Content = content;
        }

        public override SpriteFont LoadMethod(ContentManager manager) => manager.Load<SpriteFont>(Content);
    }
}
