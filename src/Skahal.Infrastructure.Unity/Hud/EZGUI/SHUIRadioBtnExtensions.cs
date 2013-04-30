#region Usings
using System;

#endregion

namespace Skahal.HUD.EZGUI
{
	/// <summary>
	/// Extensions methods for EZGUI UIRadioBtn.
	/// </summary>
	public static class SHUIRadioBtnExtensions
	{
		/// <summary>
		/// Simulates the tap on UIRadioBtn.
		/// </summary>
		/// <param name='radioButton'>
		/// Radio button.
		/// </param>
		public static void SimulateTap (this UIRadioBtn radioButton)
		{
			var touchInfo = new POINTER_INFO ();
			touchInfo.evt = POINTER_INFO.INPUT_EVENT.TAP;
		
			radioButton.OnInput (touchInfo);
		}
	}
}

