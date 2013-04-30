using System;
using UnityEngine;
using Skahal.Infrastructure.Framework.Net.Messaging;
using Skahal.Infrastructure.Framework.Logging;
using Skahal.Infrastructure.Framework.Commons;

namespace Skahal.Infrastructure.Unity.Net.Messaging
{
	/// <summary>
	/// Photon messenger proxy.
	/// </summary>
	[RequireComponent(typeof(PhotonView))]
	public class PhotonMessengerProxy : MonoBehaviour 
	{
		#region Events
		/// <summary>
		/// Occurs when connected.
		/// </summary>
		public event EventHandler Connected;
		
	//	/// <summary>
	//	/// Occurs when message received.
		///// </summary>
		//public event EventHandler<MessageEventArgs> MessageReceived;
		
		/// <summary>
		/// Occurs when disconnected.
		/// </summary>
		public event EventHandler Disconnected;
		#endregion
		
		#region Fields
	//	private Action<string, object> m_sendMessageToServerAction;
	//	private Action<string, object> m_sendMessageToClientAction;
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether this instance is server.
		/// </summary>
		/// <value><c>true</c> if this instance is server; otherwise, <c>false</c>.</value>
		public bool IsServer { get; set; }
		#endregion
		
		#region Methods
		private void Start ()
		{
			DontDestroyOnLoad (this);
			LogService.Debug ("LanMessengerProxy: IsServer: {0}", IsServer);
		}

		void OnJoinedLobby()
		{
			LogService.Debug("PhotonMessengerProxy.OnJoinedLobby.");
		}

//		/// <summary>
//		/// Sends the message to player.
//		/// </summary>
//		/// <param name="id">Identifier.</param>
//		/// <param name="value">Value.</param>
//		public void SendMessageToPlayer (string id, object value)
//		{
//			if (IsServer) {
//				SendMessageToClient (id, value);
//			} else {
//				SendMessageToServer (id, value);
//			}
//		}
		
		
//		[RPC]
//		private void SendMessageToServer (string id, object value)
//		{
//			m_sendMessageToServerAction(id, value);	
//		}
//		
//		[RPC]
//		private void SendMessageToClient (string id, object value)
//		{
//			m_sendMessageToClientAction (id, value);
//		}
		
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
}