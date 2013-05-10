#region Usings
using System;
using System.Collections;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Net.Messaging;
using UnityEngine;
using Skahal.Infrastructure.Framework.Logging;
using Skahal.Infrastructure.Framework;


#endregion

/// <summary>
/// Lan messenger proxy.
/// </summary>
[RequireComponent(typeof(NetworkView))]
public class LanMessengerProxy : MonoBehaviour
{
	#region Events
	/// <summary>
	/// Occurs when connected.
	/// </summary>
	public event EventHandler Connected;

	/// <summary>
	/// Occurs when message received.
	/// </summary>
	public event EventHandler<MessageEventArgs> MessageReceived;

	/// <summary>
	/// Occurs when disconnected.
	/// </summary>
	public event EventHandler Disconnected;
	#endregion
	
	#region Fields
	private Action<string, string> m_sendMessageToServerAction;
	private Action<string, string> m_sendMessageToClientAction;
	#endregion
	
	#region Properties
	/// <summary>
	/// Gets or sets a value indicating whether this instance is server.
	/// </summary>
	/// <value><c>true</c> if this instance is server; otherwise, <c>false</c>.</value>
	public bool IsServer { get; set; }

	/// <summary>
	/// Gets or sets the server IP address.
	/// </summary>
	/// <value>The server IP address.</value>
	public string ServerIP { get; set; }

	/// <summary>
	/// Gets or sets the port.
	/// </summary>
	/// <value>The port.</value>
	public int Port { get; set; }
	#endregion
	
	#region Methods
	private void Start ()
	{
		DontDestroyOnLoad (this);
		LogService.Debug ("LanMessengerProxy: IsServer: {0}", IsServer);

		NetHelper.ThrowIfInvalidTcpPortNumber(Port);

		Network.Disconnect();

		if (IsServer) {
			Network.InitializeServer (1, Port, false);
		} else {
			Network.Connect (ServerIP, Port);
		}
		
		Action<string, string> receiveAction = (id, value) => {		
			MessageReceived.Raise (this, new MessageEventArgs (id, value));
		};
		
		
		if (IsServer) {
			m_sendMessageToServerAction = receiveAction;
			
			m_sendMessageToClientAction = (id, value) => {
				networkView.RPC ("SendMessageToClient", RPCMode.Others, id, value);
			};
			
		} else {
			m_sendMessageToServerAction = (id, value) => {
				networkView.RPC ("SendMessageToServer", RPCMode.Others, id, value);
			};
			
			m_sendMessageToClientAction = receiveAction;
		}
	}

	/// <summary>
	/// Sends the message to player.
	/// </summary>
	/// <param name="id">Identifier.</param>
	/// <param name="value">Value.</param>
	public void SendMessageToPlayer (string id, string value)
	{
		if (IsServer) {
			SendMessageToClient (id, value);
		} else {
			SendMessageToServer (id, value);
		}
	}


	[RPC]
	private void SendMessageToServer (string id, string value)
	{
		m_sendMessageToServerAction(id, value);	
	}
	
	[RPC]
	private void SendMessageToClient (string id, string value)
	{
		m_sendMessageToClientAction (id, value);
	}
	
	private void OnConnectedToServer ()
	{
		Connected.Raise (this);
	}
	
	private void OnPlayerConnected ()
	{
		Connected.Raise (this);
	}
	
	private void OnDisconnectedFromServer ()
	{
		Disconnected.Raise (this);
	}
	
	private void OnPlayerDisconnected ()
	{
		Disconnected.Raise (this);
	}
	#endregion
}