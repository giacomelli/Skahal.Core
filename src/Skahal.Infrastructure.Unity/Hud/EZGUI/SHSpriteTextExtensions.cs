#region Usings
using UnityEngine;
using System.Collections;
#endregion

namespace Skahal.HUD.EZGUI
{
	/// <summary>
	/// Extensions methods for EZGUI SpriteText.
	/// </summary>
	public static class SHSpriteTextExtensions
	{
		#region Methods
		#region RemoveFromStart
		public static void RemoveFromStart (this SpriteText sprite, float firstDelay, float charDelay)
		{
			SH.StartCoroutine(DelayRemoveFromStart (sprite, firstDelay, charDelay));
		}
	
		static IEnumerator DelayRemoveFromStart (SpriteText sprite, float firstDelay, float charDelay)
		{
			yield return new WaitForSeconds(firstDelay);
		
			// Needs this check, because in scene unload or load the sprite can be disposed.
			while (sprite != null && sprite.Text.Length > 0)
			{
				sprite.Text = sprite.Text.Substring (1, sprite.Text.Length - 1);
				yield return new WaitForSeconds(charDelay);
			}
		}
		#endregion
		
		#region RemoveFromEnd
		public static void RemoveFromEnd (this SpriteText sprite, float firstDelay, float charDelay)
		{
			SH.StartCoroutine (DelayRemoveFromEnd (sprite, firstDelay, charDelay));
		}
		
		static IEnumerator DelayRemoveFromEnd (SpriteText sprite, float firstDelay, float charDelay)
		{
			yield return new WaitForSeconds(firstDelay);
			
			// Needs this check, because in scene unload or load the sprite can be disposed.
			while (sprite != null && sprite.Text.Length > 0) {
				sprite.Text = sprite.Text.Substring (0, sprite.Text.Length - 1);
				yield return new WaitForSeconds(charDelay);
			}
		}
		#endregion
		
		/// <summary>
		/// Shakes the the text on X.
		/// </summary>
		/// <param name='sprite'>
		/// Sprite.
		/// </param>
		/// <param name='amount'>
		/// Amount shake on X.
		/// </param>
		/// <param name='time'>
		/// The time in seconds.
		/// </param>
		public static void ShakeX (this SpriteText sprite, float amount = 0.1f, float time = 0.5f)
		{
			iTweenHelper.ShakePosition (sprite.gameObject, 
				iT.ShakePosition.x, amount,
				iT.ShakePosition.time, time);
		}
		
		public static void ShakeY (this SpriteText sprite, float amount = 0.1f, float time = 0.5f)
		{
			iTweenHelper.ShakePosition (sprite.gameObject, 
				iT.ShakePosition.y, amount,
				iT.ShakePosition.time, time);
		}
		#endregion
	}
}