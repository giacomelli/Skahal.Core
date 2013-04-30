#region Usings
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// Proxy for EZGUI Label (SpriteText).
/// </summary>
public class SHEZGUILabelProxy : SHEZGUIControlProxyBase<SpriteText>
{
	#region Constructors
	public SHEZGUILabelProxy(object control) : base(control)
	{
	}
	#endregion
		
		
	#region ISHHUDControlProxy implementation
	public override void SetText (string text)
	{
		Control.Text = text;
	}
	#endregion
}

