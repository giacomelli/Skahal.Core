#region Usings
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// iTween helper.
/// </summary>
public static class iTweenHelper
{	
	#region Move
	/// <summary>
	/// Moves to.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void MoveTo(GameObject target, params object[] argsKeyValue)
	{		
		iTween.MoveTo(target, CreateHashParams(argsKeyValue));
	}

	/// <summary>
	/// Moves from.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void MoveFrom(GameObject target, params object[] argsKeyValue)
	{
		iTween.MoveFrom(target, CreateHashParams(argsKeyValue));
	}
	#endregion
	
	#region Scale
	/// <summary>
	/// Scales from.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void ScaleFrom(GameObject target, params object[] argsKeyValue)
	{
		iTween.ScaleFrom(target, CreateHashParams(argsKeyValue));
	}

	/// <summary>
	/// Scales to.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void ScaleTo(GameObject target, params object[] argsKeyValue)
	{
		iTween.ScaleTo(target, CreateHashParams(argsKeyValue));
	}
	#endregion
	
	#region Color
	/// <summary>
	/// Change color to.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void ColorTo(GameObject target, params object[] argsKeyValue)
	{
		iTween.ColorTo(target, CreateHashParams(argsKeyValue));
	}

	/// <summary>
	/// Change color from.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void ColorFrom(GameObject target, params object[] argsKeyValue)
	{
		iTween.ColorFrom(target, CreateHashParams(argsKeyValue));
	}
	#endregion
	
	#region Shake
	/// <summary>
	/// Shakes the position.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void ShakePosition(GameObject target, params object[] argsKeyValue)
	{
		iTween.ShakePosition(target, CreateHashParams(argsKeyValue));
	}

	/// <summary>
	/// Shakes the scale.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void ShakeScale(GameObject target, params object[] argsKeyValue)
	{
		iTween.ShakeScale(target, CreateHashParams(argsKeyValue));
	}

	/// <summary>
	/// Shakes the rotation.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void ShakeRotation(GameObject target, params object[] argsKeyValue)
	{
		iTween.ShakeRotation(target, CreateHashParams(argsKeyValue));
	}
	#endregion
	
	#region Ratation
	/// <summary>
	/// Rotates to.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void RotateTo(GameObject target, params object[] argsKeyValue)
	{
		iTween.RotateTo(target, CreateHashParams(argsKeyValue));
	}

	/// <summary>
	/// Rotates from.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void RotateFrom(GameObject target, params object[] argsKeyValue)
	{
		iTween.RotateFrom(target, CreateHashParams(argsKeyValue));
	}

	/// <summary>
	/// Rotates by.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void RotateBy(GameObject target, params object[] argsKeyValue)
	{
		iTween.RotateBy(target, CreateHashParams(argsKeyValue));
	}
	#endregion
	
	#region Value
	/// <summary>
	/// Change value to.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void ValueTo(GameObject target, params object[] argsKeyValue)
	{
		iTween.ValueTo(target, CreateHashParams(argsKeyValue));
	}
	#endregion
	
	#region Camera
	/// <summary>
	/// Fades the camera to.
	/// </summary>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void CameraFadeTo(params object[] argsKeyValue)
	{
		iTween.CameraFadeTo(CreateHashParams(argsKeyValue));
	}

	/// <summary>
	/// Fades the camera from.
	/// </summary>
	/// <param name="argsKeyValue">Arguments key value.</param>
	public static void CameraFadeFrom(params object[] argsKeyValue)
	{
		iTween.CameraFadeFrom(CreateHashParams(argsKeyValue));
	}
	#endregion

	#region Private methods
	private static Hashtable CreateHashParams(params object[] argsKeyValue)
	{
		Hashtable args = new Hashtable();
		
		for (int i = 0; i < argsKeyValue.Length; i += 2)
		{
			args.Add(argsKeyValue[i], argsKeyValue[i + 1]);
		}
		
		return args;
	}
	#endregion
}

