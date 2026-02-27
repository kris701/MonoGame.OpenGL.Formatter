using FormMatter.Builders;
using FormMatter.OpenGL.Audio;
using FormMatter.OpenGL.Fonts;
using FormMatter.OpenGL.Textures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Reflection;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace FormMatter.OpenGL.ResourcePacks
{
	/// <summary>
	/// Event that fired when the pack tries to load mods
	/// </summary>
	/// <param name="folder"></param>
	public delegate void ResourcePackOnLoadModEventHandler(DirectoryInfo folder);

	/// <summary>
	/// Controlls to dynamically load texture, font, sound and music resources
	/// </summary>
	public class ResourcePackController
	{
		/// <summary>
		/// Event that fired when the pack tries to load mods
		/// </summary>
		public ResourcePackOnLoadModEventHandler? OnLoadMod;

		/// <summary>
		/// Builder to handle all the resource packs
		/// </summary>
		public ResourceBuilder<ResourcePackDefinition> ResourcePacks = new ResourceBuilder<ResourcePackDefinition>("ResourcePacks", Assembly.GetEntryAssembly());

		private readonly AudioController _audio;
		private readonly TextureController _textures;
		private readonly FontController _fonts;

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="audio"></param>
		/// <param name="textures"></param>
		/// <param name="fonts"></param>
		public ResourcePackController(AudioController audio, TextureController textures, FontController fonts)
		{
			_audio = audio;
			_textures = textures;
			_fonts = fonts;
		}

		/// <summary>
		/// Get all resource pack IDs
		/// </summary>
		/// <returns></returns>
		public List<Guid> GetResourcePacks() => ResourcePacks.GetResources();

		/// <summary>
		/// Get a resource pack by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ResourcePackDefinition GetResourcePack(Guid id) => ResourcePacks.GetResource(id);

		/// <summary>
		/// Load resources from a given pack ID
		/// </summary>
		/// <param name="texturePack"></param>
		public void LoadResourcePack(Guid texturePack)
		{
			LoadResourcePack(ResourcePacks.GetResource(texturePack));
		}

		private void LoadResourcePack(ResourcePackDefinition pack)
		{
			if (pack.BasedOn != null)
				LoadResourcePack(ResourcePacks.GetResource((Guid)pack.BasedOn));
			foreach (var item in pack.Textures)
				_textures.LoadTexture(item);
			foreach (var item in pack.TextureSets)
				_textures.LoadTextureSet(item);
			foreach (var item in pack.Songs)
				_audio.LoadSong(item);
			foreach (var item in pack.SoundEffects)
				_audio.LoadSoundEffect(item);
			foreach (var item in pack.Fonts)
				_fonts.LoadFont(item);
		}

		/// <summary>
		/// Load mods from a given folder
		/// </summary>
		/// <param name="path"></param>
		public void LoadMods(string path)
		{
			foreach (var folder in new DirectoryInfo(path).GetDirectories())
			{
				OnLoadMod?.Invoke(folder);

				foreach (var subFolder in folder.GetDirectories())
				{
					if (subFolder.Parent == null)
						continue;
					if (subFolder.Name.ToUpper() == "TEXTURES")
					{
						foreach (var file in subFolder.GetFiles())
						{
							var textureDef = JsonSerializer.Deserialize<List<TextureDefinition>>(File.ReadAllText(file.FullName));
							if (textureDef != null)
							{
								foreach (var texture in textureDef)
								{
									texture.Content = Path.Combine("..", path, folder.Name, subFolder.Name, "Content", texture.Content);
									_textures.LoadTexture(texture);
								}
							}
						}
					}
					else if (subFolder.Name.ToUpper() == "TEXTURESETS")
					{
						foreach (var file in subFolder.GetFiles())
						{
							var textureSetDef = JsonSerializer.Deserialize<List<TextureSetDefinition>>(File.ReadAllText(file.FullName));
							if (textureSetDef != null)
							{
								foreach (var textureSet in textureSetDef)
								{
									for (int i = 0; i < textureSet.Contents.Count; i++)
										textureSet.Contents[i] = Path.Combine("..", path, folder.Name, subFolder.Name, "Content", textureSet.Contents[i]);
									foreach (var content in textureSet.Contents)
										_textures.LoadTextureSet(textureSet);
								}
							}
						}
					}
					else if (subFolder.Name.ToUpper() == "SONGS")
					{
						foreach (var file in subFolder.GetFiles())
						{
							var songsDef = JsonSerializer.Deserialize<List<SongDefinition>>(File.ReadAllText(file.FullName));
							if (songsDef != null)
							{
								foreach (var song in songsDef)
								{
									song.Content = Path.Combine("..", path, folder.Name, subFolder.Name, "Content", song.Content);
									_audio.LoadSong(song);
								}
							}
						}
					}
					else if (subFolder.Name.ToUpper() == "FONTS")
					{
						foreach (var file in subFolder.GetFiles())
						{
							var fontDef = JsonSerializer.Deserialize<List<FontDefinition>>(File.ReadAllText(file.FullName));
							if (fontDef != null)
							{
								foreach (var font in fontDef)
								{
									font.Content = Path.Combine("..", path, folder.Name, subFolder.Name, "Content", font.Content);
									_fonts.LoadFont(font);
								}
							}
						}
					}
					else if (subFolder.Name.ToUpper() == "SOUNDEFFECTS")
					{
						foreach (var file in subFolder.GetFiles())
						{
							var soundEffectsDef = JsonSerializer.Deserialize<List<SoundEffectDefinition>>(File.ReadAllText(file.FullName));
							if (soundEffectsDef != null)
							{
								foreach (var soundEffect in soundEffectsDef)
								{
									soundEffect.Content = Path.Combine("..", path, folder.Name, subFolder.Name, "Content", soundEffect.Content);
									_audio.LoadSoundEffect(soundEffect);
								}
							}
						}
					}
				}
			}
		}
	}
}