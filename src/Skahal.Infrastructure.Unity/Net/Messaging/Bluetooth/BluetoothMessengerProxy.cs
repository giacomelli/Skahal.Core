#region Usings
using UnityEngine;
using System.Collections;
using Skahal.Infrastructure.Framework.Net.Messaging;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Logging;
#endregion

/// <summary>
/// Bluetooth messenger proxy.
/// </summary>
public class BluetoothMessengerProxy : MonoBehaviour
{
	#region Events
	/// <summary>
	/// Occurs when connected.
	/// </summary>
	public event System.EventHandler Connected;

	/// <summary>
	/// Occurs when disconnected.
	/// </summary>
	public event System.EventHandler Disconnected;

	/// <summary>
	/// Occurs when message received.
	/// </summary>
	public event System.EventHandler<MessageEventArgs> MessageReceived;
	#endregion
	
	#region Life cycle
	private void Start ()
	{
		DontDestroyOnLoad (this);
	}
	
	private void OnBluetoothConnected ()
	{
		LogService.Debug("BluetoothMessengerProxy.OnBluetoothConnected");
		Connected.Raise (this);
	}
	
	private void OnBluetoothDisconnected ()
	{
		LogService.Debug ("BluetoothMessengerProxy.OnBluetoothDisconnected");
		Disconnected.Raise (this);
	}
	
	private void OnMessageReceived (string serializedMessage)
	{
		LogService.Debug ("BluetoothMessengerProxy.OnMessageReceived({0})", serializedMessage);
		var msg = MessageConverter.ToMessage (serializedMessage);
		MessageReceived.Raise (this, new MessageEventArgs (msg));
	}
	#endregion
}