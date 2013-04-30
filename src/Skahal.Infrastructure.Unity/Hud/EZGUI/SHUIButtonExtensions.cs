#region Usings
using System;

#endregion

namespace Skahal.HUD.EZGUI
{
	/// <summary>
	/// Extensions methods for EZGUI UIButton.
	/// </summary>
	public static class SHUIButtonExtensions
	{
		#region Methods
		/// <summary>
		/// Simulates a tap on the button.
		/// </summary>
		/// <param name='button'>
		/// Button.
		/// </param>
		public static void SimulateTap (this UIButton button)
		{
			var touchInfo = new POINTER_INFO ();
			touchInfo.evt = POINTER_INFO.INPUT_EVENT.TAP;
		
			button.OnInput (touchInfo);
		}
		
		/// <summary>
		/// Shakes the the text on X.
		/// </summary>
		/// <param name='button'>
		/// The button.
		/// </param>
		/// <param name='amount'>
		/// Amount shake on X.
		/// </param>
		/// <param name='time'>
		/// The time in seconds.
		/// </param>
		public static void ShakeX (this UIButton button, float amount = 0.1f, float time = 0.5f)
		{
			iTweenHelper.ShakePosition (button.gameObject, 
				iT.ShakePosition.x, amount,
				iT.ShakePosition.time, time);
		}
		#endregion
	}
}