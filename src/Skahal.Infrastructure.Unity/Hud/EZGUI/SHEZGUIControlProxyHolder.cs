#region Usings
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// Allow that a EZGUI control be proxy by a script.
/// </summary>
[AddComponentMenu("Skahal/HUD/SHEZGUIControlProxyHolder")]
public class SHEZGUIControlProxyHolder : SHHUDControlProxyHolderBase
{
	#region Fields
	private SHEZGUILabelProxy m_controlProxy;
	#endregion
	
	#region Properties
	public SpriteText Label;
	#endregion
	
	#region Methods
	private void Awake ()
	{
		if (Label != null)
		{
			m_controlProxy = new SHEZGUILabelProxy(Label);
		}
	}
	#endregion
	
	#region implemented abstract members of SHHUDControlProxyHolderBase
	public override ISHHUDControlProxy ControlProxy {
		get {
			return m_controlProxy;
		}
	}
	#endregion
}