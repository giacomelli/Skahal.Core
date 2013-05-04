#region Usings
using System;
using UnityEngine;
using Skahal.Infrastructure.Framework.Net.Messaging;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Unity.Net.Messaging.GlobalServer;
using Skahal.Infrastructure.Framework.Logging;
#endregion

/// <summary>
/// Global server P2P messaging strategy.
/// </summary>
public class GlobalServerP2PMessagingStrategy : MonoBehaviour, IGlobalServerMessagingStrategy
{
	#region Constants
#if UNITY_EDITOR
	public static int P2PPort = 8182;
#else
	/// <summary>
	/// The P2P port.
	/// </summary>
	public static int P2PPort = UnityEngine.Random.Range(8000, 8999);
#endif
	private const string P2PControllerGameObjectName = "GlobalServerP2PController";
	#endregion
	
	#region Events
	/// <summary>
	/// Occurs when message received.
	/// </summary>
	public event EventHandler<MessageEventArgs> MessageReceived;

	/// <summary>
	/// Occurs when peer disconnected.
	/// </summary>
	public event EventHandler PeerDisconnected;
	#endregion
	
	#region Fields
	private NetworkView m_net;
	private bool m_raisePeerDisconnected;
	#endregion
	
	#region Properties
	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static GlobalServerP2PMessagingStrategy Instance { get; private set; }

	/// <summary>
	/// Gets the player address.
	/// </summary>
	/// <value>The player address.</value>
	public string PlayerAddress { 
		get { 
			return String.Format ("{0}:{1}", Network.player.externalIP, Network.player.externalPort);
		}
	}
	#endregion

	#region Methods
	/// <summary>
	/// Initialize this instance.
	/// </summary>
	public void Initialize ()
	{
		var go = GameObject.Find (P2PControllerGameObjectName);
		
		if (go == null)
		{
			go = new GameObject (P2PControllerGameObjectName);
			go.AddComponent<GlobalServerP2PMessagingStrategy> ();
		}
	}
	
	private void Awake ()
	{
		DontDestroyOnLoad (this);
		m_net = gameObject.AddComponent<NetworkView> ();
		m_net.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
		m_net.observed = null;
		Instance = this;
	}
	
	private void OnPlayerDisconnected (NetworkPlayer player)
	{
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
		RaisePeerDisconnected ();
	}
	
	private void OnDisconnectedFromServer ()
	{
		RaisePeerDisconnected();
	}
	
	private void RaisePeerDisconnected ()
	{
		if (m_raisePeerDisconnected)
		{
			PeerDisconnected.Raise (this);
		}
	}

	/// <summary>
	/// Sends the message.
	/// </summary>
	/// <param name="message">Message.</param>
	/// <param name="value">Value.</param>
	public void SendMessage (string message, string value)
	{
		if (Network.connections.Length > 0)
		{
			m_net.RPC ("OnMessageReceived", RPCMode.Others, message, value == null ? String.Empty : value);
		}
	}

	/// <summary>
	/// Receives the message.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="value">Value.</param>
	public void ReceiveMessage (string name, string value)
	{
		MessageReceived.Raise (this, new MessageEventArgs (name, value));
	}

	/// <summary>
	/// Connect the specified isHost and enemy.
	/// </summary>
	/// <param name="isHost">If set to <c>true</c> is host.</param>
	/// <param name="enemy">Enemy.</param>
	public bool Connect (bool isHost, GlobalServerPlayer enemy)
	{						
		NetworkConnectionError netResult = NetworkConnectionError.NoError;
								
		if (isHost)
		{
			LogService.Debug ("GlobalServerClient: trying to make P2P as a SERVER to IP: {0}...", enemy.IP);
		
			var useNat = !Network.HavePublicAddress ();
			Network.InitializeServer (32, P2PPort, useNat);	
		}
		else
		{
			LogService.Debug ("GlobalServerClient: trying to make P2P as a CLIENT to IP: {0}...", enemy.IP);
			var IPParts = enemy.IP.Split (':');
			netResult = Network.Connect (IPParts [0], Convert.ToInt32 (IPParts [1]));
			LogService.Debug ("GlobalServerClient: result '{0}'.", netResult);
		}
			
		m_raisePeerDisconnected = true;
		return  netResult != NetworkConnectionError.NoError;
	}

	/// <summary>
	/// Disconnect this instance.
	/// </summary>
	public void Disconnect ()
	{
		// If this device has explicit calling Disconnect command, so must not raise PeerDisconnected.
		m_raisePeerDisconnected = false;
		Network.Disconnect ();
	}
	
	[RPC]
	private void OnMessageReceived(string messageName, string messageValue)
	{
		MessageReceived.Raise(this, new MessageEventArgs(messageName, messageValue));
	}
	#endregion
}

