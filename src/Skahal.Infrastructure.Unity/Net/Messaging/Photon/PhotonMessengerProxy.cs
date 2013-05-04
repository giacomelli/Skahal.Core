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
		private PhotonView m_photonView;
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="Skahal.Infrastructure.Unity.Net.Messaging.PhotonMessengerProxy"/> has created a room.
		/// </summary>
		/// <value><c>true</c> if room created; otherwise, <c>false</c>.</value>
		public bool RoomCreated { get; private set; }
		#endregion
		
		#region Methods
		private void Start ()
		{
			DontDestroyOnLoad (this);

			m_photonView = GetComponent<PhotonView>();
			m_photonView.viewID = 9999;
		}

		private void Initialize()
		{	
			Action<string, string> receiveAction = (id, value) => {		
				MessageReceived.Raise (this, new MessageEventArgs (id, value));
			};
			
			if (RoomCreated) {
				m_sendMessageToServerAction = receiveAction;
				
				m_sendMessageToClientAction = (id, value) => {
					m_photonView.RPC ("SendMessageToClient", PhotonTargets.Others, id, value);
				};
				
			} else {
				m_sendMessageToServerAction = (id, value) => {
					m_photonView.RPC ("SendMessageToServer",  PhotonTargets.Others, id, value);
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
			if (RoomCreated) {
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

		void OnJoinedLobby()
		{
			LogService.Debug("PhotonMessengerProxy.OnJoinedLobby.");
			PhotonNetwork.JoinRandomRoom();
		}
		
		void OnPhotonRandomJoinFailed()
		{
			LogService.Debug("PhotonMessengerProxy.OnPhotonRandomJoinFailed.");
			RoomCreated = true;
			PhotonNetwork.CreateRoom(null);
		}
		
		void OnPhotonPlayerConnected()
		{
			LogService.Debug("PhotonMessengerProxy.OnPhotonPlayerConnected.");
			Initialize();
			Connected.Raise (this);
		}

		void OnJoinedRoom()
		{
			LogService.Debug("PhotonMessengerProxy.OnJoinedRoom.");

			if(!RoomCreated)
			{
				Initialize();
				Connected.Raise (this);
			}
		}

		private void OnPhotonPlayerDisconnected()
		{
			Disconnected.Raise (this);
		}
		#endregion
	}
}