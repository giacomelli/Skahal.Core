#region Usings
using System;
using UnityEngine;
#endregion

namespace Skahal.HUD.EZGUI
{
	/// <summary>
	/// Stuffs to EZGUI UIButton
	/// </summary>
	public static class SHUIButton
	{
		#region Methods
		/// <summary>
		/// Simulates the tap on button.
		/// </summary>
		/// <param name='gameObjectPath'>
		/// Game object path.
		/// </param>
		public static void SimulateTap (string gameObjectPath)
		{
			Find (gameObjectPath).SimulateTap ();
		}
		
		/// <summary>
		/// Find the UIButton on specified GameObject..
		/// </summary>
		/// <param name='gameObjectPath'>
		/// Game object path.
		/// </param>
		public static UIButton Find(string gameObjectPath)
		{
			return GameObject.Find (gameObjectPath).GetComponent<UIButton> ();
		}
		#endregion
	}
}

