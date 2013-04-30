#region Usings
using UnityEngine;
using System.Collections;
using System;
#endregion

public class SHEZGUIButtonProxy : SHEZGUIControlProxyBase<UIButton>
{
	#region Constructors
	public SHEZGUIButtonProxy(object control) : base(control)
	{
	}
	#endregion
		
	#region ISHHUDControlProxy implementation
	public override bool HasClickSound()
	{
		return Control.soundOnClick != null;
	}

	public override void SetClickSound (AudioSource sound)
	{
		Control.soundOnClick = sound;
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