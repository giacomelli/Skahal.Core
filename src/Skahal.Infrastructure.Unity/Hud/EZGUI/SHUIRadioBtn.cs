                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           #region Usings
using System;
using UnityEngine;

#endregion

namespace Skahal.HUD.EZGUI
{
	/// <summary>
	/// Helper for EZGUI UIRadioBtn.
	/// </summary>
	public static  class SHUIRadioBtn
	{
		/// <summary>
		/// Find the UIRadioBtn on specified GameObject.
		/// </summary>
		/// <param name='gameObjectPath'>
		/// Game object path.
		/// </param>
		public static UIRadioBtn Find (string gameObjectPath)
		{
			return GameObject.Find (gameObjectPath).GetComponent<UIRadioBtn> ();
		}
	}
}

