#region Usings
using UnityEngine;
#endregion

namespace Skahal.HUD.EZGUI
{
	/// <summary>
	/// Extensions methods for EZGUI SpriteRoot.
	/// </summary>
	public static class SHSpriteRootExtensions
	{
		/// <summary>
		/// Changes the texture.
		/// </summary>
		/// <param name='sprite'>
		/// Sprite.
		/// </param>
		/// <param name='image'>
		/// Image.
		/// </param>
		public static void ChangeTexture (this SpriteRoot sprite, Texture2D image)
		{
			sprite.SetTexture (image);
			sprite.SetUVs (new Rect (0, 0, 1, 1));
			sprite.SetSize(sprite.width, sprite.height * ((float)image.height / (float)image.width));
		}
	}
}