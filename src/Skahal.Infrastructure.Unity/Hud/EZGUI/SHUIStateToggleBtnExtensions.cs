#region Usings
using System;
using UnityEngine;
#endregion

namespace Skahal.HUD.EZGUI
{
	/// <summary>
	/// Extensions methods for EZGUI UIStateToggleBtn.
	/// </summary>
	public static class SHUIStateToggleBtnExtensions
	{
		/// <summary>
		/// Simulates a tap on the button.
		/// </summary>
		/// <param name='button'>
		/// Button.
		/// </param>
		public static void SimulateTap(this UIStateToggleBtn button)
		{
			var touchInfo = new POINTER_INFO ();
			touchInfo.evt = POINTER_INFO.INPUT_EVENT.TAP;
		
			button.OnInput (touchInfo);
		}
	}
}