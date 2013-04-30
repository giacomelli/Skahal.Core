#region Usings
using System;
using System.Collections;
using UnityEngine;
#endregion

public class SHEZGUIToggleButtonProxy : SHEZGUIControlProxyBase<UIStateToggleBtn>
{
	#region Constructors
	public SHEZGUIToggleButtonProxy (object control) : base(control)
	{
	}
	#endregion
	
	#region ISHHUDControlProxy implementation
	public override bool HasClickSound ()
	{
		return Control.soundToPlay != null;
	}

	public override void SetClickSound (AudioSource sound)
	{
		Control.soundToPlay = sound;
	}
	
	public override bool HasClickAction ()
	{
		return !String.IsNullOrEmpty (Control.methodToInvoke);
	}
	
	public override void SetText (string text)
	{
		Control.Text = text;
	}
	#endregion
}