using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace MonoGame.OpenGL.Formatter.Audio
{
	/// <summary>
	/// Structural definition of a sound effect
	/// </summary>
	public class SoundEffectDefinition : LoadableContent<SoundEffect>
	{
		/// <summary>
		/// Unique ID of this sound effect
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
		public SoundEffectDefinition(Guid iD, string content, bool isDefered) : base(isDefered)
		{
			ID = iD;
			Content = content;
		}

		/// <summary>
		/// Load the given sound effect.
		/// </summary>
		/// <param name="manager"></param>
		/// <returns></returns>
		public override SoundEffect LoadMethod(ContentManager manager) => manager.Load<SoundEffect>(Content);
	}
}