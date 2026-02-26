namespace MonoGame.FormMatter.Builders
{
	/// <summary>
	/// An item identifiably by an unique ID
	/// </summary>
	public interface IIdentifiable
	{
		/// <summary>
		/// A unique ID
		/// </summary>
		public Guid ID { get; set; }
	}
}
