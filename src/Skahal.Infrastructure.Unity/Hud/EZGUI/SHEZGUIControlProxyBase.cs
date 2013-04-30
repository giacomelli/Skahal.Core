#region Usings
using UnityEngine;
using System.Collections;
#endregion

public class SHEZGUIControlProxyBase<TControl> : ISHHUDControlProxy
{
	#region Constructors
	protected SHEZGUIControlProxyBase (object control)
	{
		Control = (TControl)control;
	}
	#endregion
	
	#region Properties
	protected TControl Control { get; set; }
	#endregion
	
	#region ISHHUDControlProxy implementation
	public virtual bool HasClickSound()
	{
		return false;
	}

	public virtual void SetClickSound(AudioSource sound)
	{
		Debug.LogWarning ("SHEZGUIControlProxyBase: attempt to set sound on a control that does not support sounds!");
	}

	public virtual bool HasClickAction()
	{
		return false;
	}

	public virtual void SetText(string text)
	{
		Debug.LogWarning ("SHEZGUIControlProxyBase: attempt to set text on a control that does not support texts!");
	}
	#endregion
}