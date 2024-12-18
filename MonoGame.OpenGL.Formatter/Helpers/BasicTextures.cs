﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.OpenGL.Formatter.Helpers
{
	/// <summary>
	/// A helper class to generate simple textures.
	/// </summary>
	public static class BasicTextures
	{
		private static GraphicsDevice? _graphicsDevice;
		private static readonly Dictionary<Color, Texture2D> _rectangleCache = new Dictionary<Color, Texture2D>();
		private static readonly Dictionary<CircleKey, Texture2D> _circleCache = new Dictionary<CircleKey, Texture2D>();

		/// <summary>
		/// Initialize this helper with the games graphics adapter
		/// </summary>
		/// <param name="graphicsDevice"></param>
		public static void Initialize(GraphicsDevice graphicsDevice)
		{
			_graphicsDevice = graphicsDevice;
		}

		/// <summary>
		/// Get a 1x1 texture of a solid color
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Texture2D GetBasicRectange(Color target)
		{
			if (_rectangleCache.ContainsKey(target))
				return _rectangleCache[target];
			Texture2D texture = new Texture2D(_graphicsDevice, 1, 1);
			texture.SetData(new[] { target });
			_rectangleCache.Add(target, texture);
			return texture;
		}

		/// <summary>
		/// Get a "clicked" texture color, being gray with low opacity
		/// </summary>
		/// <returns></returns>
		public static Texture2D GetClickedTexture() => GetBasicRectange(new Color(64, 64, 64, 100));

		private class CircleKey
		{
			public Color Color { get; set; }
			public int Radius { get; set; }

			public CircleKey(Color color, int radius)
			{
				Color = color;
				Radius = radius;
			}

			public override bool Equals(object? obj)
			{
				if (obj is CircleKey other)
				{
					if (other.Color != Color) return false;
					if (other.Radius != Radius) return false;
					return true;
				}
				return false;
			}

			public override int GetHashCode()
			{
				return HashCode.Combine(Color, Radius);
			}
		}

		/// <summary>
		/// Get a filled circle of some color and radius
		/// </summary>
		/// <param name="target"></param>
		/// <param name="radius"></param>
		/// <returns></returns>
		public static Texture2D GetBasicCircle(Color target, int radius)
		{
			var key = new CircleKey(target, radius);
			if (_circleCache.ContainsKey(key))
				return _circleCache[key];
			var texture = new Texture2D(_graphicsDevice, radius, radius);
			var colorData = new Color[radius * radius];

			float diam = radius / 2f;
			float diamsq = diam * diam;

			for (int x = 0; x < radius; x++)
			{
				for (int y = 0; y < radius; y++)
				{
					int index = x * radius + y;
					Vector2 pos = new Vector2(x - diam, y - diam);
					if (pos.LengthSquared() <= diamsq)
						colorData[index] = target;
					else
						colorData[index] = Color.Transparent;
				}
			}

			texture.SetData(colorData);
			_circleCache.Add(key, texture);
			return texture;
		}
	}
}