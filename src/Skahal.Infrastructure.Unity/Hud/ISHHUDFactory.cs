#region Usings
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// Defines a  HUD factory.
/// </summary>
public interface ISHHUDFactory
{
	#region Methods
	ISHHUDControlProxy CreateButtonProxy(object control);
	ISHHUDControlProxy CreateRadioButtonProxy (object control);
	ISHHUDControlProxy CreateToggleButtonProxy (object control);
	ISHControlFinder CreateControlFinder();
	#endregion
}