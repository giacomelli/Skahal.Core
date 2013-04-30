#region Usings
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// An EZGUI control finder.
/// </summary>
/// </exception>
public class SHEZGUIControlFinder : ISHControlFinder
{
	#region ISHControlFinder implementation
	/// <summary>
	/// Finds the buttons.
	/// </summary>
	/// <returns>
	/// The buttons.
	/// </returns>
	public object[] FindButtons ()
	{
		return GameObject.FindObjectsOfType (typeof(UIButton));
	}
	
	/// <summary>
	/// Finds the radio buttons.
	/// </summary>
	/// <returns>
	/// The radio buttons.
	/// </returns>
	public object[] FindRadioButtons ()
	{
		return GameObject.FindObjectsOfType (typeof(UIRadioBtn));
	}
	
	/// <summary>
	/// Finds the toggle buttons.
	/// </summary>
	/// <returns>
	/// The toggle buttons.
	/// </returns>
	public object[] FindToggleButtons()
	{
		return GameObject.FindObjectsOfType (typeof(UIStateToggleBtn));
	}
	#endregion
}