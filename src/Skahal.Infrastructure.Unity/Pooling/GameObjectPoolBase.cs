#region Usings
using System;
using System.Collections;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Logging;
using UnityEngine;
using Skahal.Infrastructure.Framework.Pooling;
#endregion

/// <summary>
/// A base class for game object pools
/// </summary>
public abstract class GameObjectPoolBase : MonoBehaviour, IPool
{
	#region Fields
	private List<GameObject> m_gameObjects;
	private Transform m_container;
	#endregion
	
	#region Editor properties
	/// <summary>
	/// The name.
	/// </summary>
	public string m_name;

	/// <summary>
	/// The size.
	/// </summary>
	public int m_size = 10;

	/// <summary>
	/// The size is fixed?
	/// </summary>
	public bool m_isFixedSize = false;

	/// <summary>
	/// The auto disable time.
	/// <remarks>
	/// If value is diff than zero,
	/// then the object will be put back to the pool in seconds defined in the value.
	/// </remarks>
	/// </summary>
	public float m_autoDisableTime = 0;

	/// <summary>
	/// If should raise OnGameObjectEnabledInPool message.
	/// </summary>
	public bool m_raiseOnGameObjectEnabledInPoolMessage = false;

	/// <summary>
	/// If should raise OnGameObjectDisabledInPool message.
	/// </summary>
	public bool m_raiseOnGameObjectDisabledInPoolMessage = false;
   #endregion

   #region Properties
	/// <summary>
	/// Gets the game objects count.
	/// </summary>
	/// <value>
	/// The game objects count.
	/// </value>
	public int GameObjectsCount {
		get {
			return m_gameObjects.Count;
		}
	}

	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <value>The name.</value>
	public string Name { get; private set; }
	
	/// <summary>
	/// Gets or sets the size.
	/// </summary>
	/// <value>The size.</value>
	public int Size { get; set; }
	
	/// <summary>
	/// Gets or sets a value indicating whether this instance is fixed size.
	/// </summary>
	/// <value>true</value>
	/// <c>false</c>
	public bool IsFixedSize { get; set; }
	
	/// <summary>
	/// Gets or sets the auto disable time.
	/// </summary>
	/// <value>The auto disable time.</value>
	public float AutoDisableTime { get; set; }

	/// <summary>
	/// Gets the items count.
	/// </summary>
	/// <value>The items count.</value>
	public int ItemsCount { get; private set; }
	#endregion
	
	#region Internals
	private void Awake()
	{
		Name = m_name;
	}

	/// <summary>
	/// Creates the initial items on the pool.
	/// </summary>
	/// <returns>
	/// The objects.
	/// </returns>
	public void CreateItems ()
	{
		m_gameObjects = new List<GameObject> (m_size);
		
		for (int i = 0; i < m_size; i++) {
			AddGameObject ();
		}
	}

	/// <summary>
	/// Adds a game object to the pool.
	/// </summary>
	/// <returns>
	/// The game object added.
	/// </returns>
	private GameObject AddGameObject ()
	{
		GameObject go = CreateObject ();
		go.transform.parent = m_container;
		ReleaseItem (go);
		m_gameObjects.Add (go);

		m_container.gameObject.name = String.Format ("{0} ({1})", m_name, m_gameObjects.Count);
		ItemsCount++;

		return go;
	}

	/// <summary>
	/// Gets the game object from the pool.
	/// </summary>
	public object GetItem ()
	{
		int length = m_gameObjects.Count;
		GameObject go = null;
		
		for (int i = 0; i < length; i++) {
			go = m_gameObjects [i];

			if (go == null) {
				LogService.Error ("{0} - GameObject on index {1} is null. You should not call Destroy() in objects that are in a pool.", GetType ().Name, i);
			}

			if (IsObjectEnabled (go)) {
				go = null;
			} else {
				break;
			}
		}
		
		if (go == null) {
			if (m_isFixedSize) {
				go = m_gameObjects [0];
				m_gameObjects.RemoveAt (0);
				m_gameObjects.Add (go);
			} else {
				go = AddGameObject ();
			}
		}
		
		EnableObject(go as GameObject);
		
		return go;
	}

	/// <summary>
	/// Enables the game object.
	/// </summary>
	/// <param name='go'>
	/// Go.
	/// </param>
	/// <param name='position'>
	/// Position.
	/// </param>
	private void GetItem (GameObject go, Vector3 position)
	{
		go.transform.position = position;
		EnableObject (go);
		go.name = m_name + " (IN USE)";
		
		if (m_raiseOnGameObjectEnabledInPoolMessage) {
			//Messenger.Send ("OnGameObjectEnabledInPool", go);
		}
		
		if (m_autoDisableTime > 0) {
			StartCoroutine (AutoDisable (go, m_autoDisableTime));
		}
	}

	/// <summary>
	/// Auto the disable the game object when delay time is reached.
	/// </summary>
	/// <param name='go'>
	/// Go.
	/// </param>
	/// <param name='delay'>
	/// Delay.
	/// </param>
	private IEnumerator AutoDisable (GameObject go, float delay)
	{
		yield return new WaitForSeconds (delay);
		ReleaseItem (go);
	}

	/// <summary>
	/// Releases the item.
	/// </summary>
	/// <param name="item">Item.</param>
	public void ReleaseItem (object item)
	{
		LogService.Debug ("SHPoolBase.ReleaseGameObject for pool {0}...", m_name);
		
		lock (this) {
			var go = item as GameObject;
			DisableObject (go);
			go.name = m_name + " (FREE)";
			go.transform.parent = m_container;
			
			if (m_raiseOnGameObjectDisabledInPoolMessage) {
				//Messenger.Send ("OnGameObjectDisabledInPool", go);
			}
		}
	}
	
	/// <summary>
	/// Releases all items that fit in release filter specified.
	/// </summary>
	/// <param name='releaseFilter'>
	/// Release filter.
	/// </param>
	public void ReleaseAll (Func<object, bool> releaseFilter)
	{
		foreach (var go in m_gameObjects) {
			if (releaseFilter (go)) {
				ReleaseItem (go);}
		}
	}

	/// <summary>
	/// Sets the container.
	/// </summary>
	/// <param name='container'>
	/// Container.
	/// </param>
	public void SetContainer (Transform container)
	{
		m_container = container;
	}
	#endregion
	
	#region Implements in every pool
	/// <summary>
	/// Creates the object.
	/// </summary>
	/// <returns>
	/// The object.
	/// </returns>
	protected abstract GameObject CreateObject ();

	/// <summary>
	/// Disables the object.
	/// </summary>
	protected abstract void DisableObject (GameObject goInPool);

	/// <summary>
	/// Enables the object.
	/// </summary>
	protected abstract void EnableObject (GameObject goInPool);

	/// <summary>
	/// Determines whether specified object is enabled.
	/// </summary>
	protected abstract bool IsObjectEnabled (GameObject goInPool);
	#endregion
}

