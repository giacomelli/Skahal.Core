#region Usings
using UnityEngine;
using System.Collections;

#endregion

/// <summary>
/// Implements a classic pool for game objects derived from a Unity particle system's prefab.
/// </summary>
[AddComponentMenu("Skahal/Pooling/ParticleSystemPrefabPool")]
public class ParticleSystemPrefabPool : PrefabPool
{	
	/// <summary>
	/// Disables the object.
	/// </summary>
	/// <param name="goInPool">Go in pool.</param>
	protected override void DisableObject (GameObject goInPool)
	{
		var ps = goInPool.GetComponent<ParticleSystem> ();
		ps.Stop ();
		ps.Clear (true);
		
		var childrenComponents = goInPool.GetComponentsInChildren<ParticleSystem> ();
		
		foreach (var c in childrenComponents) {
			c.Stop();
			c.Clear (true);
		}
		
		base.DisableObject (goInPool);
	}
}