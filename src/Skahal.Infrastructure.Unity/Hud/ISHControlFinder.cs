#region Usings
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// Defines a UI control finder.
/// </summary>
public interface ISHControlFinder
{
	#region Methods
	/// <summary>
	/// Finds the buttons.
	/// </summary>
	/// <returns>
	/// The buttons.
	/// </returns>
	object[] FindButtons();
	
	/// <summary>
	/// Finds the radio buttons.
	/// </summary>
	/// <returns>
	/// The radio buttons.
	/// </returns>
	object[] FindRadioButtons();
	
	/// <summary>
	/// Finds the toggle buttons.
	/// </summary>
	/// <returns>
	/// The toggle buttons.
	/// </returns>
	object[] FindToggleButtons();
	#endregion
}

