#region Usings
using System;
using System.Collections;
using UnityEngine;
using Skahal.Infrastructure.Framework.Commons;
#endregion

/// <summary>
/// A helper class to generic stuffs on Unity.
/// </summary>
public class SH : MonoBehaviour
{
	#region Events
	/// <summary>
	/// Occurs when application paused.
	/// </summary>
	public static event EventHandler ApplicationPaused;
	
	/// <summary>
	/// Occurs when application resumed.
	/// </summary>
	public static event EventHandler ApplicationResumed;
	
	/// <summary>
	/// Occurs when a level is loaded.
	/// </summary>
	public static event EventHandler LevelLoaded;
	#endregion
	
	#region Fields
	private static MonoBehaviour s_script;
	#endregion
	
	#region Constructors
	static SH()
	{
		ValidateState();
	}
	#endregion
	
	#region Life cycle
	void Awake ()
	{
		s_script = this;
		DontDestroyOnLoad (this);
	}	
	#endregion
	
	#region StartCoroutine
	/// <summary>
	/// Starts the coroutine.
	/// </summary>
	/// <param name="routine">Routine.</param>
	public static new void StartCoroutine (IEnumerator routine)
	{
		ValidateState ();
		s_script.StartCoroutine (routine);
	}
	#endregion
	
	#region Sleep
	/// <summary>
	/// Sleep the specified seconds.
	/// </summary>
	/// <param name="seconds">Seconds.</param>
	public static void Sleep (float seconds)
	{
		ValidateState();
		s_script.StartCoroutine (ExecuteSleep (seconds));
	}
	
	static IEnumerator ExecuteSleep(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}
	#endregion
	
	#region Events stuffs
	void OnApplicationPause (bool pause)
	{
		if (pause) {
			ApplicationPaused.Raise (s_script);
		} else {
			ApplicationResumed.Raise (s_script);
		}
	}
	
	void OnLevelWasLoaded ()
	{
		LevelLoaded.Raise(s_script);
	}
	#endregion
	
	#region Helpers
	private static void ValidateState ()
	{
		if (s_script == null) {
			s_script = new GameObject("SH").AddComponent<SH>();
		}
	}
	#endregion
}

