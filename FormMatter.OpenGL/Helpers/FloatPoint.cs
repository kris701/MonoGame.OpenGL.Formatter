namespace FormMatter.OpenGL.Helpers
{
	/// <summary>
	/// Class for handling floating point positions
	/// </summary>
	public class FloatPoint
	{
		/// <summary>
		/// X value 
		/// </summary>
		public float X { get; set; }
		/// <summary>
		/// Y value
		/// </summary>
		public float Y { get; set; }

		/// <summary>
		/// Main constructor
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public FloatPoint(float x, float y)
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// Nicer to look at ToString override.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return $"{X}, {Y}";
		}
	}
}