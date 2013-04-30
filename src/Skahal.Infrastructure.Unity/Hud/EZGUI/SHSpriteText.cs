                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          #region Usings#region Uings#
using UnityEngine;
using System.Collections;
#endregion

namespace Skahal.HUD.EZGUI
{
	/// <summary>
	/// SH sprite text.
	/// </summary>
	public static class SHSpriteText
	{
		/// <summary>
		/// Find the SpritText component in the specified GameObject path.
		/// </summary>
		/// <param name='gameObjectPath'>
		/// Game object path.
		/// </param>
		public static SpriteText Find (string gameObjectPath)
		{
			return GameObject.Find (gameObjectPath).GetComponent<SpriteText> ();
		}
	}
}