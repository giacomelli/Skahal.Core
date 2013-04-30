#region Usings
using System;
using System.Collections;
using System.Threading;
using UnityEngine;
#endregion

/// <summary>
/// Threading facilities.
/// </summary>
public static class SHThread 
{
	#region Public methods
	/// <summary>
	/// Start the specified action in parallel.
	/// </summary>
	public static void Start (float delay, Action action)
	{
		ValidateTimeScale ();	
		
		if (delay == 0) {
			action ();
		} else {
			SH.StartCoroutine (Run (delay, action));	
		}
	}
	
	/// <summary>
	/// Repeat the specified action with interval between from and to values.
	/// </summary>
	public static void Repeat (float interval, float from, float to, Action<float> action)
	{
		ValidateTimeScale ();
		SH.StartCoroutine (RunRepeat (interval, from, to, action));
	}
	
	/// <summary>
	/// Performs a ping-pong repeat action with interval between from and to values.
	/// </summary>
	public static void PingPong (float interval, float from, float to, Func<float, bool> action)
	{
		ValidateTimeScale ();
		SH.StartCoroutine (RunPingPong (interval, from, to, action));
	}
	
	/// <summary>
	/// Performs a loop repeat action with interval between from and to values.
	/// </summary>
	public static void Loop (float interval, float from, float to, Func<float, bool> action)
	{
		ValidateTimeScale ();
		SH.StartCoroutine (RunLoop (interval, from, to, action));
	}
	
	/// <summary>
	/// Waits for the waitForCondition function returns true.
	/// </summary>
	public static void WaitFor (float delay, Func<bool> waitForCondition, Action waitEndedAction)
	{
		Start(delay, () => {
			SH.StartCoroutine (RunWaitFor (waitForCondition, waitEndedAction));	
		});
	}
	
	/// <summary>
	/// Waits for the waitForCondition function returns true.
	/// </summary>
	public static void WaitFor (Func<bool> waitForCondition, Action waitEndedAction)
	{
		ValidateTimeScale ();
		SH.StartCoroutine (RunWaitFor (waitForCondition, waitEndedAction));	
	}
	#endregion
	
	#region Private methods
	private static void ValidateTimeScale ()
	{
		if (Time.timeScale <= 0)
		{
			Debug.LogError ("SHThread: Time.timeScale should be greater than zero to use this method.");
		}
	}
	
	private static IEnumerator Run (float delay, Action action)
	{
		yield return new WaitForSeconds(delay);
		
		try
		{
			action ();
		}
		catch(Exception ex)
		{
			Debug.LogWarning("SHThread.Start: error while executing action - " + ex.Message);
		}
	}
	
	private static IEnumerator RunRepeat (float interval, float from, float to, Action<float> action)
	{
		for (float i = from; i < to; i++) {
			action (i);
			yield return new WaitForSeconds(interval);
		}
	}
	
	private static IEnumerator RunPingPong (float interval, float from, float to, Func<float, bool> action)
	{
		bool isCancelled = false;
			
		for (float i = from; i < to; i++) {
			
			if (!action (i)) {
				isCancelled = true;
				break;
			}
		
			yield return new WaitForSeconds(interval);
		}
		
		if (!isCancelled) {
			for (float i = to - 1; i >= from; i--) {
			
				if (!action (i)) {
					isCancelled = true;
					break;
				}
		
				yield return new WaitForSeconds(interval);
			}
		
			if (!isCancelled) {
				SH.StartCoroutine (RunPingPong (interval, from, to, action));
			}
		}
	}
	
	private static IEnumerator RunLoop (float interval, float from, float to, Func<float, bool> action)
	{
		bool isCancelled = false;
			
		for (float i = from; i < to; i++) {
			
			if (!action (i)) {
				isCancelled = true;
				break;
			}
		
			yield return new WaitForSeconds(interval);
		}	
		
		if (!isCancelled) {
			SH.StartCoroutine (RunLoop (interval, from, to, action));
		}
	
	}
	
	private static IEnumerator RunWaitFor (Func<bool> waitForCondition, Action waitEndedAction)
	{
		while (!waitForCondition()) {
			yield return new WaitForSeconds(1f);
		}	
		
		waitEndedAction ();
	}
	#endregion
}