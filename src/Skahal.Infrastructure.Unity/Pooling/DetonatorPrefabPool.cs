#region Usings
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// Implements a classic pool for game objects derived from a Detonator's prefab.
/// </summary>
[AddComponentMenu("Skahal/Pooling/DetonatorPrefabPool")]
public class DetonatorPrefabPool : PrefabPool {

	/// <summary>
	/// Enables the object.
	/// </summary>
	/// <param name="goInPool">Go in pool.</param>
	protected override void EnableObject (GameObject goInPool)
	{
		goInPool.SetActive (true);
		goInPool.SendMessage ("Explode");
	}

	/// <summary>
	/// Disables the object.
	/// </summary>
	/// <param name="goInPool">Go in pool.</param>
	protected override void DisableObject (GameObject goInPool)
	{
		base.DisableObject (goInPool);
	}
}