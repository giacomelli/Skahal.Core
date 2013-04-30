//
//  BluetoothManager.cs
//  Bluetooth Manager
//  Do not distribute or share this file.
//
//  Created by Daniel Mileusnic on 5/23/10.
//  Copyright 2010 WebGames3D.com. All rights reserved.
//

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

[AddComponentMenu ("")]
public class BluetoothManager : MonoBehaviour {
	
	#if UNITY_IPHONE
	[DllImport ("__Internal")]
	static extern void _ShowPicker ();
	[DllImport ("__Internal")]
	static extern void _HidePicker ();
	[DllImport ("__Internal")]
	static extern void _SendMessage (string go, string method, string param, bool reliable);
	[DllImport ("__Internal")]
	static extern void _CloseSession ();
	#endif
	
	static bool isLeader;
	static bool isConnected;
	
	static BluetoothManager instance;
	
	static void CreateInstance () {
		if (instance == null) {
			GameObject go = new GameObject (" -- BluetoothHandler -- ");
			go.hideFlags = HideFlags.HideAndDontSave;
			DontDestroyOnLoad (go);
			
			instance = (BluetoothManager)go.AddComponent (typeof (BluetoothManager));
		}
	}
	
	public static void ShowPicker () {
		#if UNITY_IPHONE
			if (Application.isEditor)
				Debug.Log ("BluetoothManager.Show Picker ()");
			else {
				CreateInstance ();
				_ShowPicker  ();
			}
		#else
			Debug.Log ("BluetoothManager.Show Picker ()");
		#endif
	}
	public static void HidePicker () {
		#if UNITY_IPHONE
			if (Application.isEditor)
				Debug.Log ("BluetoothManager.Hide Picker ()");
			else {
				CreateInstance ();
				_HidePicker ();
			}
		#else
			Debug.Log ("BluetoothManager.Hide Picker ()");
		#endif
	}
	public static void SendMessage (string go, string method, string param, bool reliable) {
		#if UNITY_IPHONE
			if (Application.isEditor)
				Debug.Log ("BluetoothManager.SendMessage (" + go + ", " + method + ", " + param + "," + reliable + ")");
			else {
				CreateInstance ();
				_SendMessage (go, method, param, reliable);
			}
		#else
			Debug.Log ("BluetoothManager.SendMessage (" + go + ", " + method + ", " + param + "," + reliable + ")");
		#endif
	}
	public static void SendMessage (string go, string method, bool reliable) {
		#if UNITY_IPHONE
			if (Application.isEditor)
				Debug.Log ("BluetoothManager.SendMessage (" + go + ", " + method + ", " + reliable + ")");
			else {
				CreateInstance ();
				_SendMessage (go, method, " ", reliable);
			}
		#else
			Debug.Log ("BluetoothManager.SendMessage (" + go + ", " + method + ", " + reliable + ")");
		#endif
	}
	public static void CloseSession () {
		#if UNITY_IPHONE
			if (Application.isEditor)
				Debug.Log ("BluetoothManager.CloseSession ()");
			else {
				CreateInstance ();
				_CloseSession ();
			}
		#else
			Debug.Log ("BluetoothManager.CloseSession ()");
		#endif
	}
	public static bool IsLeader () {
		#if UNITY_IPHONE
			return isLeader;
		#else
			return false;
		#endif
	}
	public static bool IsConnected () {
		#if UNITY_IPHONE
			return isConnected;
		#else
			return false;
		#endif
	}

	void Connected (string isController) {
		isConnected = true;
		
		if (isController == "0")
			isLeader = false;
		else
			isLeader = true;
		
		Object[] allGO = FindObjectsOfType (typeof (GameObject));
		foreach (GameObject go in allGO)
			go.SendMessage ("OnBluetoothConnected", SendMessageOptions.DontRequireReceiver);
	}
	void PickerCancel () {
		Object[] allGO = FindObjectsOfType (typeof (GameObject));
		foreach (GameObject go in allGO)
			go.SendMessage ("OnBluetoothPickerCancel", SendMessageOptions.DontRequireReceiver);
	}
	void Disconnected () {
		isConnected = false;
		isLeader = false;
		
		Object[] allGO = FindObjectsOfType (typeof (GameObject));
		foreach (GameObject go in allGO)
			go.SendMessage ("OnBluetoothDisconnected", SendMessageOptions.DontRequireReceiver);
	}
}