using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace MonoGame.FormMatter.OpenGL.Audio
{
	/// <summary>
	/// Structural definition of a song
	/// </summary>
	public class SongDefinition : LoadableContent<Song>
	{
		/// <summary>
		/// Unique ID of the song
		/// </summary>
		public Guid ID { get; set; }
		/// <summary>
		/// Path to the content
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="iD"></param>
		/// <param name="content"></param>
		/// <param name="isDefered"></param>
		public SongDefinition(Guid iD, string content, bool isDefered) : base(isDefered)
		{
			ID = iD;
			Content = content;
		}

		/// <summary>
		/// Load the given song
		/// </summary>
		/// <param name="manager"></param>
		/// <returns></returns>
		public override Song LoadMethod(ContentManager manager) => manager.Load<Song>(Content);
	}
}