#region Usings
using UnityEngine;
using System.Collections;

#endregion

/// <summary>
/// Implements a classic pool for game objects derived from a prefab.
/// </summary>
[AddComponentMenu("Skahal/Pooling/PrefabPool")]
public class PrefabPool : GameObjectPoolBase
{
   #region Constructors
	/// <summary>
	/// Initializes a new instance of the <see cref="PrefabPool"/> class.
	/// </summary>
	/// <param name='prefab'>
	/// Prefab.
	/// </param>
	public PrefabPool (Object prefab)
	{
		Prefab = prefab;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="PrefabPool"/> class.
	/// </summary>
	public PrefabPool ()
	{

	}
   	#endregion

   	#region Editor properties
	/// <summary>
	/// The prefab.
	/// </summary>
	public Object Prefab;
   	#endregion

   	#region implemented abstract members of SHPoolBase
	/// <summary>
	/// Creates the object.
	/// </summary>
	/// <returns>
	/// The object.
	/// </returns>
	protected override GameObject CreateObject ()
	{
		var go = (GameObject)Object.Instantiate (Prefab, Vector3.zero, Quaternion.identity);
		return go;
	}

	/// <summary>
	/// Disables the GameObject.
	/// </summary>
	/// <param name='goInPool'>
	/// GameObject in pool.
	/// </param>
	protected override void DisableObject (GameObject goInPool)
	{
		goInPool.SetActive (false);
	}

	/// <summary>
	/// Enables the GameObject.
	/// </summary>
	/// <param name='goInPool'>
	/// GameObject in pool.
	/// </param>
	protected override void EnableObject (GameObject goInPool)
	{
		goInPool.SetActive (true);
	}

	/// <summary>
	/// Determines whether the specified GameObject is enabled..
	/// </summary>
	/// <returns>
	/// <c>true</c> if the GameObject is enabled; otherwise, <c>false</c>.
	/// </returns>
	/// <param name='goInPool'>
	/// The GameObject in pool.
	/// </param>
	protected override bool IsObjectEnabled (GameObject goInPool)
	{
		return goInPool.activeSelf;
	}
   #endregion
}

