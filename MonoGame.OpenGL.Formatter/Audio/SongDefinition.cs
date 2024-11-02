﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace MonoGame.OpenGL.Formatter.Audio
{
    public class SongDefinition : LoadableContent<Song>
    {
        public Guid ID { get; set; }
        public string Content { get; set; }

        public SongDefinition(Guid iD, string content, bool isDefered) : base(isDefered)
        {
            ID = iD;
            Content = content;
        }

        public override Song LoadMethod(ContentManager manager) => manager.Load<Song>(Content);
    }
}