#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Skahal.Infrastructure.Framework.Pooling;

#endregion

/// <summary>
/// A container for game objects pools.
/// </summary>
[AddComponentMenu("Skahal/Pooling/GameObjejectPoolsContainer")]
public class GameObjejectPoolsContainer : MonoBehaviour
{	 
  	 #region Methods
	private void Start ()
	{
		GameObjectPoolBase[] pools = this.GetComponentsInChildren<GameObjectPoolBase> ();
		
		foreach (GameObjectPoolBase p in pools) {
			if (p.enabled) {
				var goName = p.m_name + " pool";

				var container = transform.FindChild (goName);

				if (container == null) {
					container = new GameObject (goName).transform;
					container.parent = gameObject.transform;
				}

				p.SetContainer (container);
				PoolService.RegisterPool (p);
				p.CreateItems ();	
			}
		}
	}

	private void OnDestroy()
	{
		PoolService.ClearAllPools();
	}
   	#endregion
}