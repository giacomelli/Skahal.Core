#region Usings
using UnityEngine;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Logging;
#endregion

namespace Skahal.Infrastructure.Unity.Commons
{
	/// <summary>
	/// Game object helper.
	/// </summary>
	public static class GameObjectHelper
	{	
		#region Methods
		/// <summary>
		/// Gets the nearest game objects (others) from the specified GameObject.
		/// </summary>
		/// <returns>The nearest game object.</returns>
		/// <param name="from">From.</param>
		/// <param name="others">Others.</param>
		public static GameObject GetNearestFrom (GameObject from, IEnumerable<GameObject> others)
		{
			LogService.Debug ("GetNearestFrom: from = " + from.name);
			GameObject nearest = null;
			float lowestDistance = float.MaxValue;
		
			foreach (GameObject go in others) {
				LogService.Debug ("GetNearestFrom: go = " + go.name);
			
				if (go.GetInstanceID () != from.GetInstanceID ()) {
					float d = Vector3.Distance (go.transform.position, from.transform.position);
				
					if (d < lowestDistance) {
						lowestDistance = d;
						nearest = go;
					}
				}
			}
		
			return nearest;
		}
	
		/// <summary>
		/// Find the GameObject with the specified name, if it does not exists, then create it.
		/// </summary>
		/// <returns>The GameObject.</returns>
		/// <param name="name">Name.</param>
		public static GameObject FindOrCreate (string name)
		{
			return FindOrCreate (name, Vector3.zero);
		}

		// <summary>
		/// Find the GameObject with the specified name, if it does not exists, then create it.
		/// </summary>
		/// <returns>The or create.</returns>
		/// <param name="name">Name.</param>
		/// <param name="componentTypes">Component types to be added to when gameobject is created.</param>/
		public static GameObject FindOrCreate (string name, params System.Type[] componentTypes)
		{
			return FindOrCreate(name, Vector3.zero, componentTypes);
		}
	
		/// <summary>
		/// Find the GameObject with the specified name, if it does not exists, then create it.
		/// </summary>
		/// <returns>The GameObject.</returns>
		/// <param name="name">Name.</param>
		public static GameObject FindOrCreate (string name, Vector3 position, params System.Type[] componentTypes)
		{
			var go = GameObject.Find (name);
		
			if (go == null) {
				go = new GameObject (name);
				go.transform.position = position;

				foreach(var c in componentTypes)
				{
					go.AddComponent(c);
				}
			}
		
			return go;
		}
	
		/// <summary>
		/// Active the specified GameObjects.
		/// </summary>
		/// <param name="gos">Gos.</param>
		/// <param name="includeChildren">If set to <c>true</c> include children.</param>
		public static void Active (GameObject[] gos, bool includeChildren = false)
		{
			foreach (GameObject g in gos) {
				if (g != null) {
					if (includeChildren) {
						g.SetActiveRecursively (true);
					} else {	
						g.active = true;
					}
				}	
			}
		}
	
		/// <summary>
		/// Deactive the specified GameObjects.
		/// </summary>
		/// <param name="gos">Gos.</param>
		/// <param name="includeChildren">If set to <c>true</c> include children.</param>
		public static void Deactive (GameObject[] gos, bool includeChildren = false)
		{
			foreach (GameObject g in gos) {
				if (g != null) {
					if (includeChildren) {
						g.SetActiveRecursively (false);
					} else {	
						g.active = false;
					}
				}
			}
		}
	
		/// <summary>
		/// Destroy the specified GameObjects.
		/// </summary>
		/// <param name="gos">Gos.</param>
		public static void Destroy (GameObject[] gos)
		{
			if (gos != null) {
				foreach (GameObject g in gos) {
					GameObject.Destroy (g);
				}
			}
		}

		/// <summary>
		/// Destroy the game object, if existis, with the specified name.
		/// </summary>
		/// <param name="name">Name.</param>
		public static bool Destroy(string name)
		{
			var go = GameObject.Find(name);

			if(go != null)
			{
				GameObject.Destroy(go);
				return true;
			}

			return false;
		}
		#endregion
	}
}
