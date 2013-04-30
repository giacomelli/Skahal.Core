#region Usings
using System;
using UnityEngine;
#endregion

namespace Skahal.HUD.EZGUI
{
	/// <summary>
	/// Helper for EZGUI UISlider
	/// </summary>
	public static class SHUISlider
	{
		/// <summary>
		/// Find the UISlider component in the GameObject with the specified path.
		/// </summary>
		/// <param name='gameObjectPath'>
		/// Game object path.
		/// </param>
		public static UISlider Find (string gameObjectPath)
		{
			return GameObject.Find (gameObjectPath).GetComponent<UISlider>();
		}
		
		/// <summary>
		/// Sets the value of the UISlider in the GameObject with the specified path.
		/// </summary>
		/// <param name='gameObjectPath'>
		/// Game object path.
		/// </param>
		/// <param name='value'>
		/// Value.
		/// </param>
		public static void SetValue (string gameObjectPath, float value)
		{
			Find(gameObjectPath).Value = value;
		}
	}
}