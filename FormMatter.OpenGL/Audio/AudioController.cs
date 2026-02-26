using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace FormMatter.OpenGL.Audio
{
	/// <summary>
	/// <seealso cref="IContentController"/> implementation for handling music and sound effects
	/// </summary>
	public class AudioController : IContentController
	{
		/// <summary>
		/// Content manager for the game
		/// </summary>
		public ContentManager ContentManager { get; }

		private readonly Dictionary<Guid, SongDefinition> _songs = new Dictionary<Guid, SongDefinition>();
		private readonly Dictionary<Guid, SoundEffectDefinition> _soundEffects = new Dictionary<Guid, SoundEffectDefinition>();
		private readonly Dictionary<Guid, SoundEffectInstance> _instances = new Dictionary<Guid, SoundEffectInstance>();
		private string _playing = "";

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="contentManager"></param>
		public AudioController(ContentManager contentManager)
		{
			ContentManager = contentManager;
		}

		/// <summary>
		/// Check if a given song ID exists
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool ContainsSong(Guid id) => _songs.ContainsKey(id);
		/// <summary>
		/// Check if a given sound effect set ID exists
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool ContainsSoundEffect(Guid id) => _soundEffects.ContainsKey(id);

		/// <summary>
		/// Load a song
		/// </summary>
		/// <param name="item"></param>
		public void LoadSong(SongDefinition item)
		{
			if (_songs.ContainsKey(item.ID))
				_songs.Remove(item.ID);
			item.LoadContent(ContentManager);
			_songs.Add(item.ID, item);
		}

		/// <summary>
		/// Load a sound effect
		/// </summary>
		/// <param name="item"></param>
		public void LoadSoundEffect(SoundEffectDefinition item)
		{
			if (_soundEffects.ContainsKey(item.ID))
				_soundEffects.Remove(item.ID);
			item.LoadContent(ContentManager);
			_soundEffects.Add(item.ID, item);
		}

		/// <summary>
		/// Play a song.
		/// This will also stop any other song playing at the same time.
		/// </summary>
		/// <param name="id"></param>
		public void PlaySong(Guid id)
		{
			if (!_songs.ContainsKey(id))
				return;
			var song = _songs[id];
			if (song.Content == _playing)
				return;
			_playing = song.Content;
			MediaPlayer.Stop();
			MediaPlayer.Play(song.GetLoadedContent());
		}

		/// <summary>
		/// Stop playing a current song
		/// </summary>
		public void StopSong()
		{
			_playing = "";
			MediaPlayer.Stop();
		}

		/// <summary>
		/// Play a sound effect on repeat
		/// </summary>
		/// <param name="id">ID of the new sound effect</param>
		/// <returns></returns>
		public Guid PlaySoundEffect(Guid id)
		{
			if (!_soundEffects.ContainsKey(id))
				return Guid.Empty;
			var newEffect = Guid.NewGuid();
			_instances.Add(newEffect, _soundEffects[id].GetLoadedContent().CreateInstance());
			_instances[newEffect].Volume = SoundEffect.MasterVolume;
			_instances[newEffect].IsLooped = true;
			_instances[newEffect].Play();
			return newEffect;
		}

		/// <summary>
		/// Play a sound effect once
		/// </summary>
		/// <param name="id"></param>
		public void PlaySoundEffectOnce(Guid id)
		{
			if (!_soundEffects.ContainsKey(id))
				return;
			_soundEffects[id].GetLoadedContent().Play();
		}

		/// <summary>
		/// Stop a given sound effect, based on the ID from the <seealso cref="PlaySoundEffect(Guid)"/> method
		/// </summary>
		/// <param name="id"></param>
		public void StopSoundEffect(Guid id)
		{
			if (!_instances.ContainsKey(id))
				return;
			_instances[id].Stop();
			_instances.Remove(id);
		}

		/// <summary>
		/// Pause all songs and sound efeects
		/// </summary>
		public void PauseSounds()
		{
			foreach (var key in _instances.Keys)
				_instances[key].Pause();
			MediaPlayer.Pause();
		}

		/// <summary>
		/// Resume all songs and sound effects
		/// </summary>
		public void ResumeSounds()
		{
			foreach (var key in _instances.Keys)
				_instances[key].Resume();
			MediaPlayer.Resume();
		}

		/// <summary>
		/// Stop all songs and sound effects
		/// </summary>
		public void StopSounds()
		{
			foreach (var key in _instances.Keys)
				_instances[key].Stop();
			_instances.Clear();
			MediaPlayer.Stop();
			_playing = "";
		}
	}
}