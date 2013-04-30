#region Usings
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// A EZGUI HUD factory.
/// </summary>
public class SHEZGUIHUDFactory : ISHHUDFactory
{
	#region ISHSoundFactory implementation

	public ISHHUDControlProxy CreateButtonProxy (object control)
	{
		return new SHEZGUIButtonProxy (control);
	}
	
	public ISHHUDControlProxy CreateRadioButtonProxy (object control)
	{
		return new SHEZGUIRadioButtonProxy (control);
	}
	
	public ISHHUDControlProxy CreateToggleButtonProxy (object control)
	{
		return new SHEZGUIToggleButtonProxy (control);
	}
	
	public ISHControlFinder CreateControlFinder ()
	{
		return new SHEZGUIControlFinder();
	}
	#endregion
}