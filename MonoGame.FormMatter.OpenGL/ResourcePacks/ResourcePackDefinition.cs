using MonoGame.FormMatter.Builders;
using MonoGame.FormMatter.OpenGL.Audio;
using MonoGame.FormMatter.OpenGL.Fonts;
using MonoGame.FormMatter.OpenGL.Textures;

namespace MonoGame.FormMatter.OpenGL.ResourcePacks
{
	/// <summary>
	/// Definition of a resource pack
	/// </summary>
	public class ResourcePackDefinition : IIdentifiable
	{
		/// <summary>
		/// ID of this pack
		/// </summary>
		public Guid ID { get; set; }
		/// <summary>
		/// Human readable name
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Pack description
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// If another pack should be loaded before this, specify here
		/// </summary>
		public Guid? BasedOn { get; set; }
		/// <summary>
		/// List of textures in this pack
		/// </summary>
		public List<TextureDefinition> Textures { get; set; }
		/// <summary>
		/// List of texture sets in this pack
		/// </summary>
		public List<TextureSetDefinition> TextureSets { get; set; }
		/// <summary>
		/// List of songs in this pack
		/// </summary>
		public List<SongDefinition> Songs { get; set; }
		/// <summary>
		/// List of sound effects in this pack
		/// </summary>
		public List<SoundEffectDefinition> SoundEffects { get; set; }
		/// <summary>
		/// List of fonts in this pack
		/// </summary>
		public List<FontDefinition> Fonts { get; set; }

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="iD"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		/// <param name="basedOn"></param>
		/// <param name="textures"></param>
		/// <param name="textureSets"></param>
		/// <param name="songs"></param>
		/// <param name="soundEffects"></param>
		/// <param name="fonts"></param>
		public ResourcePackDefinition(Guid iD, string name, string description, Guid? basedOn, List<TextureDefinition> textures, List<TextureSetDefinition> textureSets, List<SongDefinition> songs, List<SoundEffectDefinition> soundEffects, List<FontDefinition> fonts)
		{
			ID = iD;
			Name = name;
			Description = description;
			BasedOn = basedOn;
			Textures = textures;
			TextureSets = textureSets;
			Songs = songs;
			SoundEffects = soundEffects;
			Fonts = fonts;
		}
	}
}