#region Usings
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
///  A proxy for HUD controls.
/// </summary>
public interface ISHHUDControlProxy
{
	#region Methods
	bool HasClickSound();
	void SetClickSound(AudioSource sound);
	bool HasClickAction();
	void SetText(string text);
	#endregion
}

