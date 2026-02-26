using System.Reflection;
using System.Text.Json;

namespace MonoGame.FormMatter.Builders
{
	/// <summary>
	/// A class that can be used to build embedded JSON resources
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ResourceBuilder<T> where T : IIdentifiable
	{
		private readonly string _resourceDir;
		private readonly Dictionary<Guid, T> _resources = new Dictionary<Guid, T>();
		private readonly Assembly _assembly;
		private readonly JsonSerializerOptions _options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="resourceDir"></param>
		/// <param name="assembly"></param>
		public ResourceBuilder(string resourceDir, Assembly assembly)
		{
			_resourceDir = resourceDir;
			_assembly = assembly;
			Load();
		}

		private void Load()
		{
			_resources.Clear();

			var names = _assembly.GetManifestResourceNames().ToList();
			var source = $"{_assembly.GetName().Name}.{_resourceDir}";
			var resources = names.Where(str => str.StartsWith(source)).ToList();
			foreach (var resource in resources)
			{
				var item = ParseResource(resource);
				_resources.Add(item.ID, item);
			}
		}

		/// <summary>
		/// Reload all resources
		/// </summary>
		public void Reload() => Load();

		/// <summary>
		/// Load external resource
		/// </summary>
		/// <param name="items"></param>
		/// <exception cref="Exception"></exception>
		public void LoadExternalResources(List<FileInfo> items)
		{
			foreach (var item in items)
			{
				var parsed = JsonSerializer.Deserialize<T>(File.ReadAllText(item.FullName), _options);
				if (parsed == null)
					throw new Exception($"Error parsing resource: {item.FullName}");

				if (_resources.ContainsKey(parsed.ID))
					_resources[parsed.ID] = parsed;
				else
					_resources.Add(parsed.ID, parsed);
			}
		}

		/// <summary>
		/// Get all resource IDs in this builder
		/// </summary>
		/// <returns></returns>
		public List<Guid> GetResources() => _resources.Keys.ToList();

		/// <summary>
		/// Get a resource by its ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public T GetResource(Guid id) => _resources[id];

		private T ParseResource(string resource)
		{
			using (Stream? stream = _assembly.GetManifestResourceStream(resource))
			{
				if (stream == null)
					throw new Exception($"Resource not found: {resource}");
				using (StreamReader reader = new StreamReader(stream))
				{
					string result = reader.ReadToEnd();
					var item = JsonSerializer.Deserialize<T>(result, _options);
					if (item == null)
						throw new Exception($"Resource not found: {resource}");
					return item;
				}
			}
			throw new Exception($"Resource not found: {resource}");
		}
	}
}