#region Usings
using UnityEngine;
using System.Collections;
using System;
#endregion

#region Enums
/// <summary>
/// All devices generations.
/// </summary>
public enum SHDeviceGeneration
{
	/// <summary>
	/// An unknow device.
	/// </summary>
	Unknown,

	/// <summary>
	/// The iPhone.
	/// </summary>
	iPhone,

	/// <summary>
	/// The iPhone 3G.
	/// </summary>
	iPhone3G,

	/// <summary>
	/// The iPhone 3GS.
	/// </summary>
	iPhone3GS,

	/// <summary>
	/// The iPod Touch first generation.
	/// </summary>
	iPodTouch1Gen,

	/// <summary>
	/// The iPod Touch second generation.
	/// </summary>
	iPodTouch2Gen,

	/// <summary>
	/// The iPod Touch third generation.
	/// </summary>
	iPodTouch3Gen,

	/// <summary>
	/// The iPad first generation.
	/// </summary>
	iPad1Gen,

	/// <summary>
	/// The iPhone 4.
	/// </summary>
	iPhone4,

	/// <summary>
	/// The iPod fourth generation.
	/// </summary>
	iPodTouch4Gen,

	/// <summary>
	/// The iPad second generation.
	/// </summary>
	iPad2Gen,

	/// <summary>
	/// The iPhone 4S.
	/// </summary>
	iPhone4S,

	/// <summary>
	/// The Android.
	/// </summary>
	Android,

	/// <summary>
	/// The editor.
	/// </summary>
	Editor,

	/// <summary>
	/// The MacOSX.
	/// </summary>
	Mac,

	/// <summary>
	/// The windows.
	/// </summary>
	Windows,

	/// <summary>
	/// The iPad third generation.
	/// </summary>
	iPad3Gen,

	/// <summary>
	/// The web.
	/// </summary>
	Web,

	/// <summary>
	/// The iPhone 5.
	/// </summary>
	iPhone5,

	/// <summary>
	/// The iPad fourth generation.
	/// </summary>
	iPad4Gen
}

/// <summary>
/// All devices families.
/// </summary>
public enum SHDeviceFamily
{
	/// <summary>
	/// The unknown device family.
	/// </summary>
	Unknown,

	/// <summary>
	/// The iPhone happy family.
	/// </summary>
	iPhone,

	/// <summary>
	/// The iPod family.
	/// </summary>
	iPod,

	/// <summary>
	/// The iPad family.
	/// </summary>
	iPad,

	/// <summary>
	/// The Android huge family.
	/// </summary>
	Android,

	/// <summary>
	/// The Editor very small family.
	/// </summary>
	Editor,

	/// <summary>
	/// The MacOSX family.
	/// </summary>
	Mac,

	/// <summary>
	/// The Windows family.
	/// </summary>
	Windows, 

	/// <summary>
	/// The web family.
	/// </summary>
	Web
}
#endregion

/// <summary>
/// Helpers for device.
/// </summary>
public static class SHDevice
{
	#region Fields
	/// <summary>
	/// The device generation current emulated.
	/// </summary>
	private static SHDeviceGeneration s_emulatedDeviceGeneration;
	#endregion
	
	#region Properties
	/// <summary>
	/// Gets or sets a value indicating whether is emulating generation.
	/// </summary>
	/// <value>
	/// <c>true</c> if is emulating generation; otherwise, <c>false</c>.
	/// </value>
	public static bool IsEmulatingGeneration { get; private set; }
	
	/// <summary>
	/// Gets a value indicating whether current device is an iPad
	/// </summary>
	/// <value>
	/// <c>true</c> if this current device is an iPad; otherwise, <c>false</c>.
	/// </value>
	public static bool IsIPad {
		get {
			
			return Generation == SHDeviceGeneration.iPad1Gen 
				|| Generation == SHDeviceGeneration.iPad2Gen 
				|| Generation == SHDeviceGeneration.iPad3Gen 
				|| Generation == SHDeviceGeneration.iPad4Gen
				|| Generation == SHDeviceGeneration.Unknown;
		}
	}
	
	/// <summary>
	/// Gets a value indicating whether current device is an iPod.
	/// </summary>
	public static bool IsIpod {
		get {
			return Generation == SHDeviceGeneration.iPodTouch1Gen 
				|| Generation == SHDeviceGeneration.iPodTouch2Gen 
				|| Generation == SHDeviceGeneration.iPodTouch3Gen 
				|| Generation == SHDeviceGeneration.iPodTouch4Gen;
		}
	}
	
	/// <summary>
	/// Gets a value indicating whether current device is an Ma.
	/// </summary>
	public static bool IsMac {
		get {
			return Generation == SHDeviceGeneration.Mac;
		}
	}
	
	/// <summary>
	/// Gets a value indicating whether current device is an editor.
	/// </summary>
	public static bool IsEditor {
		get {
			return Generation == SHDeviceGeneration.Editor;
		}
	}
	
	/// <summary>
	/// Gets a value indicating whether current device supports vibration.
	/// <remarks>
	/// SystemInfo.supportsVibration from Unity appears does not works well when the device is iPad.
	/// </remarks>
	/// </summary>
	/// <value>
	/// <c>true</c> if supports vibration; otherwise, <c>false</c>.
	/// </value>
	public static bool SupportsVibration {
		get {
			return SystemInfo.supportsVibration && !IsIPad && !IsIpod;
		}
	}
	
	/// <summary>
	/// Gets the generation.
	/// </summary>
	/// <value>
	/// The generation.
	/// </value>
	public static SHDeviceGeneration Generation {
		get {
			if (IsEmulatingGeneration)
			{
				return s_emulatedDeviceGeneration;
			}
			else
			{
#if UNITY_EDITOR 
			return SHDeviceGeneration.Editor;
#elif UNITY_IPHONE
			return (SHDeviceGeneration)System.Enum.Parse(typeof(SHDeviceGeneration), iPhone.generation.ToString(), true);
#elif UNITY_ANDROID
			return SHDeviceGeneration.Android;	
#elif UNITY_STANDALONE_OSX
			return SHDeviceGeneration.Mac;
#elif UNITY_STANDALONE_WIN
			return SHDeviceGeneration.Windows;
#elif UNITY_WEBPLAYER
			return SHDeviceGeneration.Web;
#endif
			}
		}
	}
	
	/// <summary>
	/// Gets the family.
	/// </summary>
	/// <value>
	/// The family.
	/// </value>
	public static SHDeviceFamily Family {
		get {
			switch (Generation)
			{
			case SHDeviceGeneration.Android:
				return SHDeviceFamily.Android;
				
			case SHDeviceGeneration.Editor:
				return SHDeviceFamily.Editor;
				
			case SHDeviceGeneration.iPad1Gen:
			case SHDeviceGeneration.iPad2Gen:
			case SHDeviceGeneration.iPad3Gen:
			case SHDeviceGeneration.iPad4Gen:
				return SHDeviceFamily.iPad;
				
			case SHDeviceGeneration.iPhone:
			case SHDeviceGeneration.iPhone3G:
			case SHDeviceGeneration.iPhone3GS:
			case SHDeviceGeneration.iPhone4:
			case SHDeviceGeneration.iPhone4S:
			case SHDeviceGeneration.iPhone5:
				return SHDeviceFamily.iPhone;
				
			case SHDeviceGeneration.iPodTouch1Gen:
			case SHDeviceGeneration.iPodTouch2Gen:
			case SHDeviceGeneration.iPodTouch3Gen:
			case SHDeviceGeneration.iPodTouch4Gen:
				return SHDeviceFamily.iPod;
				
			case SHDeviceGeneration.Mac:
				return SHDeviceFamily.Mac;
				
			case SHDeviceGeneration.Windows:
				return SHDeviceFamily.Windows;
				
			case SHDeviceGeneration.Web:
				return SHDeviceFamily.Web;
				
			default:
				return SHDeviceFamily.Unknown;
			}
		}
	}
	
	#endregion
	
	#region Methods
	/// <summary>
	/// Starts the generation emulation.
	/// </summary>
	/// <param name='generation'>
	/// Generation.
	/// </param>
	public static void StartGenerationEmulation (SHDeviceGeneration generation)
	{
		s_emulatedDeviceGeneration = generation;
		IsEmulatingGeneration = true;
	}
	
	/// <summary>
	/// Stops the generation emulation.
	/// </summary>
	public static void StopGenerationEmulation ()
	{
		IsEmulatingGeneration = false;
	}
	
	/// <summary>
	/// Vibrate this instance.
	/// </summary>
	public static void Vibrate ()
	{
#if UNITY_IPHONE || UNITY_ANDROID
		Handheld.Vibrate();
#endif
	}
	#endregion
}