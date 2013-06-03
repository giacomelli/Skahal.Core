#region Usings
using System.Collections;
using System.Linq;
using ExitGames.Client.Photon;
using Skahal.Infrastructure.Framework.Logging;
using Skahal.Infrastructure.Framework.Net.Messaging;
using UnityEngine;
using Skahal.Infrastructure.Unity.Commons;
using Skahal.Infrastructure.Unity.Logging;
#endregion

namespace Skahal.Infrastructure.Unity.Net.Messaging.Photon
{
	/// <summary>
	/// A Photon IMessenger implementation.
	/// </summary>
	public class PhotonMessenger : MessengerBase
	{
		#region Fields
		private PhotonMessengerProxy m_proxy;
		#endregion
		
		#region IRemoteMessenger implementation
		public override bool CanReceiveMessages {
			get {
				return base.CanReceiveMessages;
			}
			set {
				PhotonNetwork.isMessageQueueRunning = value;
				base.CanReceiveMessages = value;
			}
		}
		/// <summary>
		/// Connect the messenger.
		/// </summary>
		public override void Connect ()
		{
			m_proxy = GameObjectHelper.FindOrCreate("PhotonMessengerProxy", typeof(PhotonMessengerProxy)).GetComponent<PhotonMessengerProxy>();
		
			m_proxy.Connected += delegate {
				OnConnected(new ConnectedEventArgs(m_proxy.RoomCreated ? 1 : 2));
			};

			m_proxy.MessageReceived += delegate(object sender, MessageEventArgs e) {
				OnMessageReceived(e);
			};
			
			m_proxy.Disconnected += delegate {
				OnDisconnected(new DisconnectedEventArgs(DisconnectionReason.RemoteQuit));
			};

			PhotonNetwork.autoJoinLobby = true;
			PhotonNetwork.ConnectUsingSettings("0.1");
		}
	
		/// <summary>
		/// Performs the send message.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		internal protected override void PerformSendMessage (string name, string value)
		{
			m_proxy.SendMessageToPlayer(name, value);
		}

		/// <summary>
		/// Performs the disconnect.
		/// </summary>
		internal protected override void PerformDisconnect ()
		{
			PhotonNetwork.Disconnect();
		}
		#endregion
	}
}