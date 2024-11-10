using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.OpenGL.Formatter.Textures
{
	/// <summary>
	/// Structural definition of a set of textures.
	/// </summary>
	public class TextureSetDefinition : LoadableContent<List<Texture2D>>
	{
		/// <summary>
		/// Unique ID of this texture set
		/// </summary>
		public Guid ID { get; set; }
		/// <summary>
		/// How much time should pass between changing texture.
		/// </summary>
		public int FrameTime { get; set; }
		/// <summary>
		/// List of paths to the different frames.
		/// </summary>
		public List<string> Contents { get; set; }

		/// <summary>
		/// Main constructor.
		/// </summary>
		/// <param name="iD"></param>
		/// <param name="frameTime"></param>
		/// <param name="contents"></param>
		/// <param name="isDefered"></param>
		public TextureSetDefinition(Guid iD, int frameTime, List<string> contents, bool isDefered) : base(isDefered)
		{
			ID = iD;
			FrameTime = frameTime;
			Contents = contents;
		}

		/// <summary>
		/// Load the content
		/// </summary>
		/// <param name="manager"></param>
		/// <returns></returns>
		public override List<Texture2D> LoadMethod(ContentManager manager)
		{
			var returnList = new List<Texture2D>();
			foreach (var content in Contents)
				returnList.Add(manager.Load<Texture2D>(content));
			return returnList;
		}
	}
}