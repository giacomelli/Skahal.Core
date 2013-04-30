                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        #region Usings
using System;
using UnityEngine;

#endregion

namespace Skahal.HUD.EZGUI
{
	/// <summary>
	/// Helper for EZGUI UIStateToggleBtn.
	/// </summary>
	public static class SHUIStateToggleBtn
	{
		/// <summary>
		/// Find the UIButtonToggleBtn on specified GameObject..
		/// </summary>
		/// <param name='gameObjectPath'>
		/// Game object path.
		/// </param>
		public static UIStateToggleBtn Find (string gameObjectPath)
		{
			return GameObject.Find (gameObjectPath).GetComponent<UIStateToggleBtn> ();
		}
	}
}