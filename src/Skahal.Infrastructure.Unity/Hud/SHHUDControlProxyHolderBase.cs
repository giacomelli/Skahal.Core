#region Usings
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// Allow that a ISHHUDControlProxy be used by a script.
/// </summary>
public abstract class SHHUDControlProxyHolderBase : MonoBehaviour
{
	#region Properties
	public abstract ISHHUDControlProxy ControlProxy { get; } 
	#endregion
}